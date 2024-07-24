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
            .NotEmpty().WithErrorCode(PostErrorCodes.CreatePost.MissingImageUrl)
            .Must(BeAValidUrl).WithErrorCode(PostErrorCodes.CreatePost.InvalidImageUrl);
    }

    private static bool BeAValidUrl(string imageUrl)
    {
        return Uri.TryCreate(imageUrl, UriKind.Absolute, out Uri? uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
