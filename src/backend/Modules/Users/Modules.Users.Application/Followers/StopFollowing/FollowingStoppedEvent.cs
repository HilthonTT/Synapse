using MediatR;

namespace Modules.Users.Application.Followers.StopFollowing;

public sealed record FollowingStoppedEvent(Guid UserId) : INotification;
