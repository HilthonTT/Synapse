using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Users.Create;

public sealed record CreateUserCommand(
    string ObjectIdentifier, 
    string Email, 
    string Name, 
    string Username, 
    string ImageUrl) : ICommand<Guid>;
