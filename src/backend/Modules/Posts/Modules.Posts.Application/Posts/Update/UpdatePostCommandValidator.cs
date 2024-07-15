using FluentValidation;

namespace Modules.Posts.Application.Posts.Update;

internal sealed class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(c => c.PostId)
            .NotEmpty().WithErrorCode(PostErrorCodes.UpdatePost.MissingPostId);

        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(PostErrorCodes.UpdatePost.MissingUserId);

        RuleFor(c => c.Title)
            .NotEmpty().WithErrorCode(PostErrorCodes.UpdatePost.MissingTitle)
            .MaximumLength(200).WithErrorCode(PostErrorCodes.UpdatePost.TitleTooLong);

        RuleFor(c => c.ImageUrl)
            .NotEmpty().WithErrorCode(PostErrorCodes.UpdatePost.MissingImageUrl);
    }
}
