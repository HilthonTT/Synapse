using MediatR;

namespace Modules.Posts.Application.Comments.Remove;

internal sealed record CommentRemovedEvent(Guid PostId) : INotification;
