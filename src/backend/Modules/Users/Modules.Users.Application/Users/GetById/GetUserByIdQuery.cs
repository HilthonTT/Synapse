using Application.Abstractions.Caching;

namespace Modules.Users.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : ICachedQuery<UserResponse>
{
    public string CacheKey => $"users-{UserId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(20);
}
