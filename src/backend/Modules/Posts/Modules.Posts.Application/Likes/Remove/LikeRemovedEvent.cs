using MediatR;

namespace Modules.Posts.Application.Likes.Remove;

internal sealed record LikeRemovedEvent(Guid PostId) : INotification;
