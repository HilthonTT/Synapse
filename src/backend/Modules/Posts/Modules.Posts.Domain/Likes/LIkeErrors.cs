using SharedKernel;

namespace Modules.Posts.Domain.Likes;

public static class LikeErrors
{
    public static readonly Error AlreadyLiked = Error.Conflict(
        "Like.AlreadyLiked", 
        "Already liked");

    public static Error NotFound(Guid userId) => Error.NotFound(
        "Like.NotFound",
        $"Like with the User Id = '{userId}' was not found");
}
