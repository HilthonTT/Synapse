using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Likes.Remove;

public sealed record RemoveLikeCommand(Guid UserId, Guid PostId) : ICommand;
