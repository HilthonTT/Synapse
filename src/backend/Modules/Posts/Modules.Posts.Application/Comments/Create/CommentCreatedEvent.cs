using MediatR;

namespace Modules.Posts.Application.Comments.Create;

public sealed record CommentCreatedEvent(Guid PostId) : INotification;
