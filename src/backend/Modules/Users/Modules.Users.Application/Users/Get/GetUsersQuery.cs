using Application.Abstractions.Caching;

namespace Modules.Users.Application.Users.Get;

public sealed record GetUsersQuery : ICachedQuery<List<UserResponse>>
{
    public string CacheKey => CacheKeys.Users.UserList;

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
