using FluentValidation;

namespace Modules.Posts.Application.Posts.Remove;

internal sealed class RemovePostCommandValidator : AbstractValidator<RemovePostCommand>
{
    public RemovePostCommandValidator()
    {
        RuleFor(c => c.PostId)
            .NotEmpty().WithErrorCode(PostErrorCodes.RemovePost.MissingPostId);

        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(PostErrorCodes.RemovePost.MissingUserId);
    }
}
