using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Comments.Create;

public sealed record CreateCommentCommand(
    Guid UserId, 
    Guid PostId, 
    string Content) : ICommand<Guid>;
