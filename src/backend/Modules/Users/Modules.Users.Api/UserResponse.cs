namespace Modules.Users.Api;

public sealed record UserResponse(Guid UserId, string Name, string Username, string ImageUrl);
