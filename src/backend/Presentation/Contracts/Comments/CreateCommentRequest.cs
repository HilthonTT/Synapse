namespace Presentation.Contracts.Comments;

public sealed record CreateCommentRequest(Guid UserId, Guid PostId, string Content);
