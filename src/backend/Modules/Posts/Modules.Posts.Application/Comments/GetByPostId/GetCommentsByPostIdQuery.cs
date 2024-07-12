using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Comments.GetByPostId;

public sealed record GetCommentsByPostIdQuery(Guid PostId) : IQuery<List<CommentResponse>>;
