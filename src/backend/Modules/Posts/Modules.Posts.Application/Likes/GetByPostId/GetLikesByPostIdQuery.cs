using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Likes.GetByPostId;

public sealed record GetLikesByPostIdQuery(Guid PostId) : IQuery<List<LikeResponse>>;
