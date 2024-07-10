using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Users.Get;

public sealed record GetUsersQuery : IQuery<List<UserResponse>>;
