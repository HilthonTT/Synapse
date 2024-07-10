using SharedKernel;

namespace Modules.Posts.Domain.Comments;

public static class CommentErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "Comment.NotFound",
        $"Comment with the Id = '{id}' was not found");
}
