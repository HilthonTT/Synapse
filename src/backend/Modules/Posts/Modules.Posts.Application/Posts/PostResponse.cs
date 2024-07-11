using Modules.Posts.Application.Comments;
using Modules.Posts.Application.Likes;

namespace Modules.Posts.Application.Posts;

public sealed record PostResponse(
    Guid Id, 
    string Title, 
    string ImageUrl, 
    string Tags,
    List<CommentResponse> Comments, 
    List<LikeResponse> Likes);
