using Application.Abstractions.Caching;
using Modules.Posts.Domain.Posts;

namespace Modules.Posts.Application.Posts.Search;

public sealed record SearchPostsQuery(
    string? SearchTerm,
    SortOrder SortOrder,
    SortColumn SortColumn,
    int Limit) : ICachedQuery<List<SearchPostResponse>>
{
    public string CacheKey => CacheKeys.Posts.Search(SearchTerm, (int)SortOrder, (int)SortColumn);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
