namespace Modules.Posts.Application.Posts.Search;

public sealed record SearchPostResponse(
    Guid Id, 
    Guid UserId,
    string Title, 
    string ImageUrl,
    string? Tags,
    string? Location, 
    int LikesCount,
    int CommentsCount,
    DateTime CreatedOnUtc, 
    DateTime? ModifiedOnUtc);
