using Application.Abstractions.Data;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedKernel;
using System.Data;

namespace Modules.Posts.Infrastructure.Outbox;

internal sealed class ProcessOutboxMessagesJob(
    IDbConnectionFactory factory,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider,
    ILogger<ProcessOutboxMessagesJob> logger) : IProcessOutboxMessagesJob
{
    private const int BatchSize = 15;
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };

    public async Task ProcessAsync()
    {
        logger.LogInformation("Beginning to process outbox messages");

        using IDbConnection connection = factory.GetOpenConnection();
        using IDbTransaction transation = connection.BeginTransaction();

        IReadOnlyList<OutboxMessageResponse> outboxMessages = await GetOutboxMessagesAsync(connection, transation);

        if (!outboxMessages.Any())
        {
            logger.LogInformation("Completed processing outbox messages - no messages to process");

            return;
        }

        foreach (OutboxMessageResponse outboxMessage in outboxMessages)
        {
            Exception? exception = null;

            try
            {
                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    SerializerSettings)!;

                await publisher.Publish(domainEvent);
            }
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "Exception while processing outbox message {MessageId}",
                    outboxMessage.Id);

                exception = caughtException;
            }

            await UpdateOutboxMessageAsync(connection, transation, outboxMessage, exception);
        }

        transation.Commit();

        logger.LogInformation("Completed processing outbox messages");
    }

    private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        const string sql =
            """
            SELECT 
                id AS Id,
                content AS Content
            FROM posts.outbox_messages
            WHERE processed_on_utc IS NULL
            ORDER BY created_on_utc
            LIMIT @BatchSize
            FOR UPDATE SKIP LOCKED
            """;

        IEnumerable<OutboxMessageResponse> outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(
            sql,
            new { BatchSize },
            transaction);

        return outboxMessages.ToList();
    }

    private Task UpdateOutboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        OutboxMessageResponse outboxMessage,
        Exception? exception)
    {
        const string sql =
            """
            UPDATE posts.outbox_messages
            SET processed_on_utc = @ProcessedOnUtc,
                error = @Error
            WHERE id = @Id;
            """;

        return connection.ExecuteAsync(
            sql,
            new
            {
                outboxMessage.Id,
                ProcessedOnUtc = dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            },
            transaction);
    }
}
