namespace Presentation.Contracts.Posts;

internal sealed record CreatePostRequest(
    Guid UserId, 
    string Title, 
    string ImageUrl, 
    string? Location, 
    string? Tags);
