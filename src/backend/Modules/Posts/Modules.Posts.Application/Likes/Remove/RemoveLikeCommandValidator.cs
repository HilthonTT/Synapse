using FluentValidation;

namespace Modules.Posts.Application.Likes.Remove;

internal sealed class RemoveLikeCommandValidator : AbstractValidator<RemoveLikeCommand>
{
    public RemoveLikeCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(LikeErrorCodes.Remove.MissingUserId);

        RuleFor(c => c.PostId)
            .NotEmpty().WithErrorCode(LikeErrorCodes.Remove.MissingPostId);
    }
}
