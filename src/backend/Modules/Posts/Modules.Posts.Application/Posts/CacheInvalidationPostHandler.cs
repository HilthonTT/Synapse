using Application.Abstractions.Caching;
using MediatR;
using Modules.Posts.Application.Posts.Create;
using Modules.Posts.Application.Posts.Remove;
using Modules.Posts.Application.Posts.Update;

namespace Modules.Posts.Application.Posts;

internal sealed class CacheInvalidationPostHandler(ICacheService cacheService) :
    INotificationHandler<PostCreatedEvent>,
    INotificationHandler<PostUpdatedEvent>,
    INotificationHandler<PostRemovedEvent>
{
    public Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.PostId, cancellationToken);
    }

    public Task Handle(PostUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.PostId, cancellationToken);
    }

    public Task Handle(PostRemovedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.PostId, cancellationToken);
    }

    private async Task HandleInternal(Guid postId, CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            cacheService.RemoveAsync($"posts-{postId}", cancellationToken),
            cacheService.RemoveKeysWithPrefixAsync("posts-cursor", cancellationToken),
            cacheService.RemoveKeysWithPrefixAsync("posts-search", cancellationToken));
    }
}
