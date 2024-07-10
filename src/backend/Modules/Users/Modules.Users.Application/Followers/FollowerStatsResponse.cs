namespace Modules.Users.Application.Followers;

public sealed record FollowerStatsResponse(Guid UserId, int FollowerCount, int FollowingCount);
