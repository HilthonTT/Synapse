namespace Modules.Posts.Application.Likes.GetByPostId;

public sealed record LikeResponse(Guid PostId, Guid UserId);
