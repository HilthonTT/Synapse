using SharedKernel;

namespace Modules.Posts.Domain.Posts;

public static class PostErrors
{
    public static readonly Error TagsMaxCount = Error.Problem(
        "Post.TagsMaxCount",
        $"You can't add more than {Post.TagsMaxCount} tags to your post");
}
