using Modules.Users.Api;

namespace Modules.Posts.Application.Comments;

public sealed record CommentResponse(
    Guid CommentId, 
    Guid PostId,
    UserResponse User, 
    DateTime CreatedOnUtc, 
    DateTime? ModifiedOnUtc);
