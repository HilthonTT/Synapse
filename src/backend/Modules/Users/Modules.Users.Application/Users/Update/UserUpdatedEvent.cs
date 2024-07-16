using MediatR;

namespace Modules.Users.Application.Users.Update;

internal sealed record UserUpdatedEvent(Guid UserId) : INotification;
