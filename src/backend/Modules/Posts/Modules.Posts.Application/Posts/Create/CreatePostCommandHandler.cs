using Application.Abstractions.Messaging;
using MediatR;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Domain.Users;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Posts.Application.Posts.Create;

internal sealed class CreatePostCommandHandler(
    IUsersApi usersApi,
    IPostRepository postRepository, 
    IPublisher publisher,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePostCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        UserApiResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        var post = Post.Create(
            request.UserId,
            request.Title,
            request.ImageUrl,
            request.Location,
            request.Tags);

        postRepository.Insert(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new PostCreatedEvent(post.Id, post.UserId), cancellationToken);

        return post.Id;
    }
}
