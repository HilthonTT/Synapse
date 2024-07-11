using SharedKernel;

namespace Modules.Posts.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "User.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static readonly Error Unauthorized = Error.Unauthorized(
        "User.Unauthorized",
        $"You aren't authorized to access this resource");
}
