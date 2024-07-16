using MediatR;

namespace Modules.Users.Application.Users.Create;

internal sealed record UserCreatedEvent(Guid UserId) : INotification;
