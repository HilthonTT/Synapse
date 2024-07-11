namespace Modules.Users.Api;

public sealed record UserResponse(Guid Id, string Name, string Username, string ImageUrl);
