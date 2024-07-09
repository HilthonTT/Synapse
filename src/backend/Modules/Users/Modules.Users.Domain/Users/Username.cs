using SharedKernel;

namespace Modules.Users.Domain.Users;

public sealed record Username
{
    public const int MaxLength = 255;

    private Username(string value) => Value = value; 

    public string Value { get; init; }

    public static Result<Username> Create(string? username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return Result.Failure<Username>(UsernameErrors.Empty);
        }

        if (username.Length > MaxLength)
        {
            return Result.Failure<Username>(UsernameErrors.TooLong);
        }

        return new Username(username);
    }
}
