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
        var tasks = new List<Task>()
        {
            cacheService.RemoveAsync(CacheKeys.Users.UserList, cancellationToken),
            cacheService.RemoveAsync(CacheKeys.Users.Id(userId), cancellationToken)
        };

        await Task.WhenAll(tasks);
    }
}
