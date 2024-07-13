namespace Presentation.Contracts.Comments;

internal sealed record UpdateCommentRequest(Guid UserId, string Content);
