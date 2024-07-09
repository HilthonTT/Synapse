using SharedKernel;

namespace Modules.Users.Domain.Users;

public sealed record ObjectIdentifier
{
    public const int MaxLength = 36;

    private ObjectIdentifier(string value) => Value = value;

    public string Value { get; init; }

    public static Result<ObjectIdentifier> Create(string? objectIdentifier)
    {
        if (string.IsNullOrWhiteSpace(objectIdentifier))
        {
            return Result.Failure<ObjectIdentifier>(
                ObjectIdentifierErrors.Empty);
        }

        if (objectIdentifier.Length > MaxLength)
        {
            return Result.Failure<ObjectIdentifier>(
                ObjectIdentifierErrors.TooLong);
        }

        return new ObjectIdentifier(objectIdentifier);
    }
}
