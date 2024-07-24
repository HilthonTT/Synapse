namespace Modules.Posts.Application.Posts;

public sealed record PostResponse(
    Guid Id, 
    string Title, 
    string ImageUrl, 
    string Tags,
    string Location,
    UserPostResponse Creator,
    int LikesCount,
    int CommentsCount);
