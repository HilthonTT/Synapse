using Application.Abstractions.Messaging;
using Modules.Users.Application.Followers.GetFollowerStats;

namespace Modules.Users.Application.Followers.GetRecentFollowers;

public sealed record GetRecentFollowersQuery(Guid UserId) : IQuery<List<FollowerResponse>>;
