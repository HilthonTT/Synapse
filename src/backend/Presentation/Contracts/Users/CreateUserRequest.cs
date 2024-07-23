namespace Presentation.Contracts.Users;

internal sealed record CreateUserRequest(
    string ObjectIdentifier,
    string Email, 
    string Username, 
    string Name, 
    string ImageUrl);
