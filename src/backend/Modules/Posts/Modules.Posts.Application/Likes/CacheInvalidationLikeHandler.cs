using Application.Abstractions.Caching;
using MediatR;
using Modules.Posts.Application.Likes.Create;
using Modules.Posts.Application.Likes.Remove;

namespace Modules.Posts.Application.Likes;

internal sealed class CacheInvalidationLikeHandler(ICacheService cacheService) :
    INotificationHandler<LikeCreatedEvent>,
    INotificationHandler<LikeRemovedEvent>
{
    public Task Handle(LikeCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.PostId, cancellationToken);
    }

    public Task Handle(LikeRemovedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.PostId, cancellationToken);
    }

    private async Task HandleInternal(Guid postId, CancellationToken cancellationToken)
    {
        await cacheService.RemoveAsync(CacheKeys.Likes.PostId(postId), cancellationToken);
    }
}
