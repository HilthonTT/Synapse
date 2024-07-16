using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Posts.Get;

public sealed record GetPostsQuery(Guid? Cursor, int Limit) : IQuery<PostsCursorResponse>;