﻿using Application.Abstractions.Messaging;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Comments;
using Modules.Posts.Domain.Users;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Posts.Application.Comments.Update;

internal sealed class UpdateCommentCommandHandler(
    IUsersApi usersApi, 
    ICommentRepository commentRepository, 
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateCommentCommand>
{
    public async Task<Result> Handle(
        UpdateCommentCommand request, 
        CancellationToken cancellationToken)
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

        if (user.UserId != comment.UserId)
        {
            return Result.Failure(UserErrors.Unauthorized);
        }

        comment.Update(request.Content);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}