using Application.Abstractions.Caching;

namespace Modules.Posts.Application.Posts.GetByUserId;

public sealed record GetPostsByUserIdQuery(Guid UserId) : ICachedQuery<List<PostResponse>>
{
    public string CacheKey => CacheKeys.Posts.UserId(UserId);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
