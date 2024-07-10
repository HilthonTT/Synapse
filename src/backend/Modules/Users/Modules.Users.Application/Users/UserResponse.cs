namespace Modules.Users.Application.Users;

public sealed record UserResponse(
    Guid Id, 
    string Name, 
    string Username, 
    DateTime CreatedOnUtc, 
    DateTime? ModifiedOnUtc);
