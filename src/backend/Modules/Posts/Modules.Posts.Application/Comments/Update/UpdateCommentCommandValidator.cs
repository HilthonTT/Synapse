using FluentValidation;

namespace Modules.Posts.Application.Comments.Update;

internal sealed class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(c => c.UserId)
           .NotEmpty().WithErrorCode(CommentErrorCodes.UpdateComment.MissingUserId);

        RuleFor(c => c.CommentId)
            .NotEmpty().WithErrorCode(CommentErrorCodes.UpdateComment.MissingCommentId);

        RuleFor(c => c.Content)
            .NotEmpty().WithErrorCode(CommentErrorCodes.UpdateComment.MissingContent)
            .MaximumLength(200).WithErrorCode(CommentErrorCodes.UpdateComment.ContentTooLong);
    }
}
