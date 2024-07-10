using Modules.Users.Domain.Users;
using SharedKernel;

namespace Modules.Users.Domain.Followers;

public sealed class FollowerService(
    IDateTimeProvider dateTimeProvider, 
    IFollowerRepository followerRepository) : IFollowerService
{
    public async Task<Result<Follower>> StartFollowingAsync(
        User user, 
        User followed, 
        CancellationToken cancellationToken = default)
    {
        if (user.Id == followed.Id)
        {
            return Result.Failure<Follower>(FollowerErrors.SameUser);
        }

        bool isAlreadyFollowing = await followerRepository.IsAlreadyFollowingAsync(user.Id, followed.Id, cancellationToken);
        if (isAlreadyFollowing)
        {
            return Result.Failure<Follower>(FollowerErrors.AlreadyFollowing);
        }

        var follower = Follower.Create(user.Id, followed.Id, dateTimeProvider.UtcNow);

        return follower;
    }

    public async Task<Result<Follower>> StopFollowingAsync(
        User user, 
        User followed, 
        CancellationToken cancellationToken = default)
    {
        if (user.Id == followed.Id)
        {
            return Result.Failure<Follower>(FollowerErrors.SameUser);
        }

        Follower? follower = await followerRepository.GetAsync(user.Id, followed.Id, cancellationToken);
        if (follower is null)
        {
            return Result.Failure<Follower>(FollowerErrors.NotFollowing);
        }

        return follower;
    }
}
