using SharedKernel;

namespace Modules.Users.Domain.Users;

public sealed record Name
{
    public const int MaxLength = 75;

    private Name(string value) => Value = value;

    public string Value { get; init; }

    public static Result<Name> Create(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Name>(NameErrors.Empty);
        }

        if (name.Length > MaxLength)
        {
            return Result.Failure<Name>(NameErrors.TooLong);
        }

        return new Name(name);
    }
}
