using SharedKernel;

namespace Modules.Posts.Domain.Posts;

public static class TagErrors
{
    public static Error NotFound(string tag) => Error.NotFound(
        "Tag.NotFound",
        $"The tag '{tag}' was not found in the post, this is case sensitive");
}
