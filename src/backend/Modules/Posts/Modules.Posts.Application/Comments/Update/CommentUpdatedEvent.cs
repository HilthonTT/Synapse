using MediatR;

namespace Modules.Posts.Application.Comments.Update;

internal sealed record CommentUpdatedEvent(Guid PostId) : INotification;
