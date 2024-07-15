using FluentValidation;

namespace Modules.Posts.Application.Comments.Remove;

internal sealed class RemoveCommentCommandValidator : AbstractValidator<RemoveCommentCommand>
{
    public RemoveCommentCommandValidator()
    {
        RuleFor(c => c.UserId)
           .NotEmpty().WithErrorCode(CommentErrorCodes.UpdateComment.MissingUserId);

        RuleFor(c => c.CommentId)
            .NotEmpty().WithErrorCode(CommentErrorCodes.UpdateComment.MissingCommentId);
    }
}
