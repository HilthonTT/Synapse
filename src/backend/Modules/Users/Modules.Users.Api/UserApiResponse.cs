namespace Modules.Users.Api;

public sealed record UserApiResponse(Guid UserId, string Name, string Username, string ImageUrl);
