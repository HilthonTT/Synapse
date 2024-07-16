using Application.Abstractions.Caching;

namespace Modules.Users.Application.Users.Get;

public sealed record GetUsersQuery : ICachedQuery<List<UserResponse>>
{
    public string CacheKey => "users";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(20);
}
