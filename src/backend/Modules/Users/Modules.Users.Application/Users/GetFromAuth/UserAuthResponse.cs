namespace Modules.Users.Application.Users.GetFromAuth;

public sealed record UserAuthResponse(
    Guid Id,
    string ObjectIdentifier, 
    string Name, 
    string Username, 
    string Email, 
    string ImageUrl);
