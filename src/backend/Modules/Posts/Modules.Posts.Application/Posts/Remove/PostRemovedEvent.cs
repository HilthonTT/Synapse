using MediatR;

namespace Modules.Posts.Application.Posts.Remove;

internal sealed record PostRemovedEvent(Guid PostId, Guid UserId) : INotification;
