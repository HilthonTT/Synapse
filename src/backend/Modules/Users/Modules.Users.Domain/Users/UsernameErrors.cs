using SharedKernel;

namespace Modules.Users.Domain.Users;

public static class UsernameErrors
{
    public static readonly Error Empty = Error.Problem(
        "Username.Empty", "Username is empty");

    public static readonly Error TooLong = Error.Problem(
        "Username.TooLong", "Username is too long");
}
