using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Caching;

internal static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3),
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration)
    {
        if (expiration is null)
        {
            return DefaultExpiration;
        }

        return new() { AbsoluteExpirationRelativeToNow = expiration };
    }
}
