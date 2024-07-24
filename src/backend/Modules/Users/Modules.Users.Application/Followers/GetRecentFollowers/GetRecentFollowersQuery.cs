using Application.Abstractions.Caching;
using Modules.Users.Application.Followers.GetFollowerStats;

namespace Modules.Users.Application.Followers.GetRecentFollowers;

public sealed record GetRecentFollowersQuery(Guid UserId) : ICachedQuery<List<FollowerResponse>>
{
    public string CacheKey => CacheKeys.Followers.RecentUserId(UserId);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
