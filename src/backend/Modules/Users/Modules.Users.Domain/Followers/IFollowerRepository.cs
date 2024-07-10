namespace Modules.Users.Domain.Followers;

public interface IFollowerRepository
{
    Task<bool> IsAlreadyFollowingAsync(
        Guid userId,
        Guid followedId,
        CancellationToken cancellationToken = default);

    void Insert(Follower follower);

    void Remove(Follower follower);

    Task<Follower?> GetAsync(Guid userId, Guid followedId, CancellationToken cancellationToken = default);
}
