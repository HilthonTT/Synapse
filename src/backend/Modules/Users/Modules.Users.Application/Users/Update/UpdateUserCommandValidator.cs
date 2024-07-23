using FluentValidation;

namespace Modules.Users.Application.Users.Update;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(UserErrorCodes.UpdateUser.MissingId);

        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(UserErrorCodes.UpdateUser.MissingEmail)
            .EmailAddress().WithErrorCode(UserErrorCodes.UpdateUser.InvalidEmail);

        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(UserErrorCodes.UpdateUser.MissingName);

        RuleFor(c => c.Username)
            .NotEmpty().WithErrorCode(UserErrorCodes.UpdateUser.MissingUsername);

        RuleFor(c => c.ImageUrl)
            .NotEmpty().WithErrorCode(UserErrorCodes.UpdateUser.MissingImageUrl);
    }
}
