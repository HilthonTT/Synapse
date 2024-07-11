using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Posts.GetById;

public sealed record GetPostByIdQuery(Guid PostId) : IQuery<PostResponse>;
