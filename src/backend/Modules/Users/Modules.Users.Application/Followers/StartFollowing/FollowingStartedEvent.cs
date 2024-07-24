using MediatR;

namespace Modules.Users.Application.Followers.StartFollowing;

internal sealed record FollowingStartedEvent(Guid UserId) : INotification;
