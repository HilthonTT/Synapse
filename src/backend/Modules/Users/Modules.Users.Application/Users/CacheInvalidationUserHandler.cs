using Application.Abstractions.Caching;
using MediatR;
using Modules.Users.Application.Users.Create;
using Modules.Users.Application.Users.Update;

namespace Modules.Users.Application.Users;

internal sealed class CacheInvalidationUserHandler(ICacheService cacheService) :
    INotificationHandler<UserCreatedEvent>,
    INotificationHandler<UserUpdatedEvent>
{
    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.UserId, cancellationToken);
    }

    public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.UserId, cancellationToken);
    }

    private async Task HandleInternal(Guid userId, CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            cacheService.RemoveAsync("users", cancellationToken),
            cacheService.RemoveAsync($"users-{userId}", cancellationToken));
    }
}
