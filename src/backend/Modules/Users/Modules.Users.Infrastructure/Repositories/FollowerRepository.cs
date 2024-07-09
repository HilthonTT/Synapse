using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Followers;
using Modules.Users.Infrastructure.Database;

namespace Modules.Users.Infrastructure.Repositories;

internal sealed class FollowerRepository(UsersDbContext context) : IFollowerRepository
{
    public void Insert(Follower follower)
    {
        context.Followers.Add(follower);
    }

    public Task<bool> IsAlreadyFollowingAsync(
        Guid userId, 
        Guid followedId, 
        CancellationToken cancellationToken = default)
    {
        return context.Followers.AnyAsync(
            f => f.UserId == userId && f.FollowedId == followedId,
            cancellationToken);
    }
}
