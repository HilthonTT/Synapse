using Application.Abstractions.Caching;

namespace Modules.Posts.Application.Comments.GetByPostId;

public sealed record GetCommentsByPostIdQuery(Guid PostId) : ICachedQuery<List<CommentResponse>>
{
    public string CacheKey => CacheKeys.Comments.PostId(PostId);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
