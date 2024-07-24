using Application.Abstractions.Messaging;
using MediatR;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Domain.Users;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Posts.Application.Posts.Remove;

internal sealed class RemovePostCommandHandler(
    IUsersApi usersApi,
    IPostRepository postRepository, 
    IPublisher publisher,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemovePostCommand>
{
    public async Task<Result> Handle(RemovePostCommand request, CancellationToken cancellationToken)
    {
        UserApiResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        Post? post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null)
        {
            return Result.Failure(PostErrors.NotFound(request.PostId));
        }

        if (post.UserId != user.UserId)
        {
            return Result.Failure(UserErrors.Unauthorized);
        }

        postRepository.Remove(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new PostRemovedEvent(post.Id), cancellationToken);

        return Result.Success();
    }
}
