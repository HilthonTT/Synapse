using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Likes.Create;

public sealed record CreateLikeCommand(Guid UserId, Guid PostId) : ICommand<Guid>;
