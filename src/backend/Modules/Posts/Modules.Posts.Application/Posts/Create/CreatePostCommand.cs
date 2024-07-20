using Application.Abstractions.Idempotency;

namespace Modules.Posts.Application.Posts.Create;

public sealed record CreatePostCommand(
    Guid RequestId,
    Guid UserId, 
    string Title, 
    string ImageUrl, 
    string? Location, 
    string? Tags) : IdempotentCommand(RequestId);
