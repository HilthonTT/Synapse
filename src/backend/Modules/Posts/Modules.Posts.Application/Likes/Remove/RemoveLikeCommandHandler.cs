using Application.Abstractions.Messaging;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Likes;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Domain.Users;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Posts.Application.Likes.Remove;

internal sealed class RemoveLikeCommandHandler(
    IUsersApi usersApi, 
    ILikeRepository likeRepository, 
    IPostRepository postRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveLikeCommand>
{
    public async Task<Result> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
    {
        UserResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        Post? post = await postRepository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null)
        {
            return Result.Failure(PostErrors.NotFound(request.PostId));
        }

        Result<Like> likeResult = post.RemoveLike(request.UserId);
        if (likeResult.IsFailure)
        {
            return Result.Failure(likeResult.Error);
        }

        Like like = likeResult.Value;

        likeRepository.Remove(like);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
