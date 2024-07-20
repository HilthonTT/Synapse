using Application.Abstractions.Idempotency;
using MediatR;
using SharedKernel;

namespace Application.Abstractions.Behaviors;

internal sealed class IdempotentCommandPipelineBehavior<TRequest, TResponse>(
    IIdempotencyService idempotencyService) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IdempotentCommand
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (await idempotencyService.RequestExistsAsync(request.RequestId, cancellationToken))
        {
            return (TResponse)Result.Failure(IdempotencyErrors.AlreadyProcessed);
        }

        string requestName = typeof(TRequest).Name;

        await idempotencyService.CreateRequestAsync(request.RequestId, requestName, cancellationToken);

        TResponse response = await next();

        return response;
    }
}
