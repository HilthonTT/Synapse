using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Comments.Update;

public sealed record UpdateCommentCommand(Guid UserId, Guid CommentId, string Content) : ICommand;
