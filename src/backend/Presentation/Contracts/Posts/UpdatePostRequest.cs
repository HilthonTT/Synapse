namespace Presentation.Contracts.Posts;

internal sealed record UpdatePostRequest(
    Guid UserId, 
    string Title, 
    string ImageUrl, 
    string? Location, 
    string? Tags);
