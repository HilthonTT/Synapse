using FluentValidation;

namespace Modules.Users.Application.Followers.StopFollowing;

internal sealed class StopFollowingCommandValidator : AbstractValidator<StopFollowingCommand>
{
    public StopFollowingCommandValidator()
    {
        RuleFor(c => c.UserId)
           .NotEmpty().WithErrorCode(FollowerErrorCodes.StopFollowing.MissingUserId);

        RuleFor(c => c.FollowedId)
            .NotEmpty().WithErrorCode(FollowerErrorCodes.StopFollowing.MissingFollowedId);
    }
}
