using Application.Abstractions.Caching;
using MediatR;
using Modules.Posts.Application.Comments.Create;
using Modules.Posts.Application.Comments.Remove;
using Modules.Posts.Application.Comments.Update;

namespace Modules.Posts.Application.Comments;

internal sealed class CacheInvalidationCommentHandler(ICacheService cacheService) :
    INotificationHandler<CommentCreatedEvent>,
    INotificationHandler<CommentUpdatedEvent>,
    INotificationHandler<CommentRemovedEvent>
{
    public Task Handle(CommentCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.PostId, cancellationToken);
    }

    public Task Handle(CommentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.PostId, cancellationToken);
    }

    public Task Handle(CommentRemovedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.PostId, cancellationToken);
    }

    private async Task HandleInternal(Guid postId, CancellationToken cancellationToken)
    {
        await cacheService.RemoveAsync($"posts-comments-{postId}", cancellationToken);
    }
}
