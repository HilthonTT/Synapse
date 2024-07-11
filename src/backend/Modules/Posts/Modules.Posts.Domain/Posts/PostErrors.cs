using SharedKernel;

namespace Modules.Posts.Domain.Posts;

public static class PostErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "Post.NotFound",
        $"Post with the Id = '{id}' was not found");
}
