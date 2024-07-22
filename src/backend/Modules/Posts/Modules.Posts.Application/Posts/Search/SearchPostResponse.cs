namespace Modules.Posts.Application.Posts.Search;

public sealed record SearchPostResponse(
    Guid Id, 
    Guid UserId,
    string Title, 
    string? Tags,
    string? Location, 
    DateTime CreatedOnUtc, 
    DateTime? ModifiedOnUtc);
