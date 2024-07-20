using Application.Abstractions.Messaging;
namespace Application.Abstractions.Idempotency;

public abstract record IdempotentCommand(Guid RequestId) : ICommand<Guid>;

public abstract record IdempotentCommand<TResponse>(Guid RequestId) : ICommand<TResponse>;
