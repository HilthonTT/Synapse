using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Posts.GetByUserId;

public sealed record GetPostsByUserIdQuery(Guid UserId) : IQuery<List<PostResponse>>;
