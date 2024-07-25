using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Users.Get;

public sealed record GetUsersQuery(int? Limit) : IQuery<List<UserResponse>>;
