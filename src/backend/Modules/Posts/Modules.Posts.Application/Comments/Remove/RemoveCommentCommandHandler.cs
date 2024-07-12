﻿using Application.Abstractions.Messaging;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Comments;
using Modules.Posts.Domain.Users;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Posts.Application.Comments.Remove;

internal sealed class RemoveCommentCommandHandler(
    IUsersApi usersApi, 
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveCommentCommand>
{
    public async Task<Result> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
    {
        UserResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        Comment? comment = await commentRepository.GetByIdAsync(request.CommentId, cancellationToken);
        if (comment is null)
        {
            return Result.Failure(CommentErrors.NotFound(request.CommentId));
        }

        if (comment.UserId != user.UserId)
        {
            return Result.Failure(UserErrors.Unauthorized);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
