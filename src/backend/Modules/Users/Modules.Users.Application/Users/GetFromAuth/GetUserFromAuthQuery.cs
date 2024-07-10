using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Users.GetFromAuth;

public sealed record GetUserFromAuthQuery : IQuery<UserAuthResponse>;
