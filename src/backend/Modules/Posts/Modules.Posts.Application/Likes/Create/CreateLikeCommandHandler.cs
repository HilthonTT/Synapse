using Application.Abstractions.Messaging;
using MediatR;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Likes;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Domain.Users;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Posts.Application.Likes.Create;

internal sealed class CreateLikeCommandHandler(
    IUsersApi usersApi,
    ILikeRepository likeRepository, 
    IPostRepository postRepository, 
    IPublisher publisher,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateLikeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        UserResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        Post? post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null)
        {
            return Result.Failure<Guid>(PostErrors.NotFound(request.PostId));
        }

        Result<Like> likeResult = post.AddLike(user.UserId);
        if (likeResult.IsFailure)
        {
            return Result.Failure<Guid>(likeResult.Error);
        }

        Like like = likeResult.Value;

        likeRepository.Insert(like);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new LikeCreatedEvent(post.Id), cancellationToken);

        return like.Id;
    }
}
