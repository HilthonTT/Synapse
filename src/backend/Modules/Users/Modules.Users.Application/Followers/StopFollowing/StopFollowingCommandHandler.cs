using Application.Abstractions.Messaging;
using MediatR;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Modules.Users.Application.Followers.StopFollowing;

internal sealed class StopFollowingCommandHandler(
    IUserRepository userRepository,
    IFollowerService followerService,
    IFollowerRepository followerRepository,
    IPublisher publisher,
    IUnitOfWork unitOfWork) : ICommandHandler<StopFollowingCommand>
{
    public async Task<Result> Handle(StopFollowingCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        User? followed = await userRepository.GetByIdAsync(request.FollowedId, cancellationToken);
        if (followed is null)
        {
            return Result.Failure(UserErrors.NotFound(request.FollowedId));
        }

        Result<Follower> result = await followerService.StopFollowingAsync(
            user,
            followed,
            cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        Follower follower = result.Value;

        followerRepository.Remove(follower);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new FollowingStoppedEvent(follower.UserId), cancellationToken);

        return Result.Success();
    }
}
