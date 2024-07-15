namespace Modules.Users.Infrastructure.Outbox;

public sealed record OutboxMessageResponse(Guid Id, string Content);
