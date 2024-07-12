using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Comments.Remove;

public sealed record RemoveCommentCommand(Guid UserId, Guid CommentId) : ICommand;
