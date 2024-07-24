using Application.Abstractions.Caching;

namespace Modules.Users.Application.Followers.GetFollowerStats;

public sealed record GetFollowerStatsQuery(Guid UserId) : ICachedQuery<FollowerStatsResponse>
{
    public string CacheKey => CacheKeys.Followers.UserId(UserId);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
