using Application.Abstractions.Messaging;
using MediatR;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Comments;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Domain.Users;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Posts.Application.Comments.Create;

internal sealed class CreateCommentCommandHandler(
    IUsersApi usersApi, 
    ICommentRepository commentRepository,
    IPostRepository postRepository,
    IPublisher publisher,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateCommentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        UserResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        if (!await postRepository.ExistsAsync(request.PostId, cancellationToken))
        {
            return Result.Failure<Guid>(PostErrors.NotFound(request.PostId));
        }

        var comment = Comment.Create(
            request.PostId,
            request.UserId,
            request.Content);

        commentRepository.Insert(comment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new CommentCreatedEvent(comment.PostId), cancellationToken);

        return comment.Id;
    }
}
