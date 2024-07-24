using Application.Abstractions.Caching;
using MediatR;
using Modules.Users.Application.Followers.StartFollowing;
using Modules.Users.Application.Followers.StopFollowing;

namespace Modules.Users.Application.Followers;

internal sealed class CacheInvalidationFollowerHandler(ICacheService cacheService) :
    INotificationHandler<FollowingStartedEvent>,
    INotificationHandler<FollowingStoppedEvent>
{
    public Task Handle(FollowingStartedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.UserId, cancellationToken);
    }

    public Task Handle(FollowingStoppedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.UserId, cancellationToken);
    }

    private async Task HandleInternal(Guid userId, CancellationToken cancellationToken)
    {
        var tasks = new List<Task>()
        {
            cacheService.RemoveAsync(CacheKeys.Followers.RecentUserId(userId), cancellationToken),
            cacheService.RemoveAsync(CacheKeys.Followers.UserId(userId), cancellationToken)
        };

        await Task.WhenAll(tasks);
    }
}
