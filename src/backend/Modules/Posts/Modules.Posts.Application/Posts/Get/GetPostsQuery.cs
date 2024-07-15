using Application.Abstractions.Caching;

namespace Modules.Posts.Application.Posts.Get;

public sealed record GetPostsQuery(Guid? Cursor, int Limit = 10) : ICachedQuery<PostsCursorResponse>
{
    public string CacheKey => $"posts-cursor-{Cursor}-limit-{Limit}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
