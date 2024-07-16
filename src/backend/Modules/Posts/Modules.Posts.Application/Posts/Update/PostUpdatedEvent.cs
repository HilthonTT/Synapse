using MediatR;

namespace Modules.Posts.Application.Posts.Update;

internal sealed record PostUpdatedEvent(Guid PostId) : INotification;
