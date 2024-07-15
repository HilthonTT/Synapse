using Application.Abstractions.Caching;

namespace Modules.Posts.Application.Posts.GetById;

public sealed record GetPostByIdQuery(Guid PostId) : ICachedQuery<PostResponse>
{
    public string CacheKey => $"posts-{PostId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
