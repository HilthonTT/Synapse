using Application.Abstractions.Messaging;

namespace Modules.Posts.Application.Posts.Remove;

public sealed record RemovePostCommand(Guid UserId, Guid PostId) : ICommand;
