using FluentValidation;

namespace Modules.Posts.Application.Likes.Create;

internal sealed class CreateLikeCommandValidator : AbstractValidator<CreateLikeCommand>
{
    public CreateLikeCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(LikeErrorCodes.CreateLike.MissingUserId);

        RuleFor(c => c.PostId)
            .NotEmpty().WithErrorCode(LikeErrorCodes.CreateLike.MissingPostId);
    }
}
