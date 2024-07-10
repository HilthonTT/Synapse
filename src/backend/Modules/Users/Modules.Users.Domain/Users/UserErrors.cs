using SharedKernel;

namespace Modules.Users.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "User.NotFound",
        $"User with the Id = '{id}' was not found");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");


    public static readonly Error OidNotUnique = Error.Conflict(
        "Users.OidNotUnique",
        "The provided object identifier is not unique");
}
