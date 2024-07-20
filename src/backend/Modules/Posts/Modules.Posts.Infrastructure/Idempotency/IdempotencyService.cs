using Application.Abstractions.Idempotency;
using Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore;
using Modules.Posts.Infrastructure.Database;
using SharedKernel;

namespace Modules.Posts.Infrastructure.Idempotency;

internal sealed class IdempotencyService(
    PostsDbContext context, 
    IDateTimeProvider dateTimeProvider) : IIdempotencyService
{
    public async Task CreateRequestAsync(Guid requestId, string name, CancellationToken cancellationToken = default)
    {
        var idempotentRequest = new IdempotentRequest(requestId, name, dateTimeProvider.UtcNow);

        context.Add(idempotentRequest);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> RequestExistsAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        return await context.Set<IdempotentRequest>().AnyAsync(r => r.Id == requestId, cancellationToken);
    }
}
