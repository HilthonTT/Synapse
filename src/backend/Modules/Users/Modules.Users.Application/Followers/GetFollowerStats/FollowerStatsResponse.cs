namespace Modules.Users.Application.Followers.GetFollowerStats;

public sealed record FollowerStatsResponse(Guid UserId, long FollowerCount, long FollowingCount);
