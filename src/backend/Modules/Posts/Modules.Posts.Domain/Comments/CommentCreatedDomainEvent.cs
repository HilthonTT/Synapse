using SharedKernel;

namespace Modules.Posts.Domain.Comments;

public sealed record CommentCreatedDomainEvent(Guid CommentId) : IDomainEvent;
