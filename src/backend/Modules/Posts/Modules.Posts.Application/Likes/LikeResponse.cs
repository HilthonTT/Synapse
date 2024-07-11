namespace Modules.Posts.Application.Likes;

public sealed record LikeResponse(Guid PostId, Guid UserId);
