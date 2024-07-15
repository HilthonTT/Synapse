using FluentValidation;

namespace Modules.Posts.Application.Comments.Create;

internal sealed class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(CommentErrorCodes.CreateComment.MissingUserId);

        RuleFor(c => c.PostId)
            .NotEmpty().WithErrorCode(CommentErrorCodes.CreateComment.MissingPostId);

        RuleFor(c => c.Content)
            .NotEmpty().WithErrorCode(CommentErrorCodes.CreateComment.MissingContent)
            .MaximumLength(200).WithErrorCode(CommentErrorCodes.CreateComment.ContentTooLong);
    }
}
