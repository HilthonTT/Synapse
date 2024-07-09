using SharedKernel;

namespace Modules.Users.Domain.Users;

public static class ObjectIdentifierErrors
{
    public static readonly Error Empty = Error.Problem(
        "ObjectIdentifier.Empty", "Object identifier is empty");

    public static readonly Error TooLong = Error.Problem(
        "ObjectIdentifier.TooLong", "Object identifier is too long");
}
