using SharedKernel;

namespace Application.Abstractions.Idempotency;

internal static class IdempotencyErrors
{
    public static readonly Error AlreadyProcessed = Error.Conflict(
        "Request.AlreadyProcessed",
        "Your request has already been processed");
}
