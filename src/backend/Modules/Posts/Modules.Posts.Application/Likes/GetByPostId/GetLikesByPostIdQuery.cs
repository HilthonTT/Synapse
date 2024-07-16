using Application.Abstractions.Caching;

namespace Modules.Posts.Application.Likes.GetByPostId;

public sealed record GetLikesByPostIdQuery(Guid PostId) : ICachedQuery<List<LikeResponse>>
{
    public string CacheKey => $"posts-likes-{PostId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
