using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
