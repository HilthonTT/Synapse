using SharedKernel;

namespace Modules.Posts.Domain.Posts;

public sealed record PostCreatedDomainEvent(Guid PostId) : IDomainEvent;
