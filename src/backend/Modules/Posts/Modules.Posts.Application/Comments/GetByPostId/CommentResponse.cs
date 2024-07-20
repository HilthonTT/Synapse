using Modules.Users.Api;

namespace Modules.Posts.Application.Comments.GetByPostId;

public sealed record CommentResponse(
    Guid Id,
    Guid PostId,
    string Content,
    UserResponse User,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc);
