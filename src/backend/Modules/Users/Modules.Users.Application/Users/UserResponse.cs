namespace Modules.Users.Application.Users;

public sealed record UserResponse(
    Guid Id, 
    string Name, 
    string Username, 
    string ImageUrl,
    DateTime CreatedOnUtc, 
    DateTime? ModifiedOnUtc);
