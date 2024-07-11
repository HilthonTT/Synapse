using SharedKernel;

namespace Modules.Posts.Domain.Likes;

public sealed record LikeCreatedDomainEvent(Guid PostId, Guid UserId) : IDomainEvent;
