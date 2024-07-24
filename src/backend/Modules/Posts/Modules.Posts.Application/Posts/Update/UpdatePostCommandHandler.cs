using Application.Abstractions.Messaging;
using MediatR;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Domain.Users;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Posts.Application.Posts.Update;

internal sealed class UpdatePostCommandHandler(
    IUsersApi usersApi,
    IPostRepository postRepository,
    IPublisher publisher,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePostCommand>
{
    public async Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        UserApiResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
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

        post.Update(
            request.Title,
            request.ImageUrl,
            request.Location,
            request.Tags);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new PostUpdatedEvent(post.Id), cancellationToken);

        return Result.Success();
    }
}
