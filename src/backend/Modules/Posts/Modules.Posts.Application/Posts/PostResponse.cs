using Modules.Users.Api;

namespace Modules.Posts.Application.Posts;

public sealed record PostResponse(
    Guid Id, 
    string Title, 
    string ImageUrl, 
    string Tags,
    string Location,
    UserResponse Creator,
    int LikesCount,
    int CommentsCount);
