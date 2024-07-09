using SharedKernel;

namespace Modules.Users.Domain.Users;

public static class NameErrors
{
    public static readonly Error Empty = Error.Problem(
        "Name.Empty", "Name is empty");

    public static readonly Error TooLong = Error.Problem(
        "Name.TooLong", "Name is too long");
}
