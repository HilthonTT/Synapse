using Application.Abstractions.Caching;

namespace Modules.Users.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : ICachedQuery<UserResponse>
{
    public string CacheKey => CacheKeys.Users.Id(UserId);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
