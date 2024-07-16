using MediatR;

namespace Modules.Posts.Application.Likes.Create;

internal sealed record LikeCreatedEvent(Guid PostId) : INotification;
