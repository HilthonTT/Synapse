using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Posts.Update;

public sealed record UpdatePostCommand(
    Guid PostId, 
    Guid UserId,
    string Title, 
    string ImageUrl, 
    string? Location,
    string? Tags) : ICommand;
