using MediatR;

namespace Modules.Posts.Application.Posts.Create;

internal sealed record PostCreatedEvent(Guid PostId) : INotification;
