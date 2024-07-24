namespace Modules.Posts.Application.Posts;

public sealed record UserPostResponse(Guid UserId, string Name, string Username, string ImageUrl);
