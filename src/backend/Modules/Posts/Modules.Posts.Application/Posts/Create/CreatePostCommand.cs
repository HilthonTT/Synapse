using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Posts.Create;

public sealed record CreatePostCommand(
    Guid UserId, 
    string Title, 
    string ImageUrl, 
    string? Location, 
    string? Tags) : ICommand<Guid>;
