using FluentValidation;

namespace Modules.Posts.Application.Posts.Create;

internal sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(PostErrorCodes.CreatePost.MissingUserId);

        RuleFor(c => c.Title)
            .NotEmpty().WithErrorCode(PostErrorCodes.CreatePost.MissingTitle)
            .MaximumLength(200).WithErrorCode(PostErrorCodes.CreatePost.TitleTooLong);

        RuleFor(c => c.ImageUrl)
            .NotEmpty().WithErrorCode(PostErrorCodes.CreatePost.MissingImageUrl);
    }
}
