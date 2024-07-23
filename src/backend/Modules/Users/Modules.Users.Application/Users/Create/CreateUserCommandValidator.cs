using FluentValidation;

namespace Modules.Users.Application.Users.Create;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.ObjectIdentifier)
            .NotEmpty().WithErrorCode(UserErrorCodes.CreateUser.MissingObjectIdentifier);

        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(UserErrorCodes.CreateUser.MissingEmail)
            .EmailAddress().WithErrorCode(UserErrorCodes.CreateUser.InvalidEmail);

        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(UserErrorCodes.CreateUser.MissingName);

        RuleFor(c => c.Username)
            .NotEmpty().WithErrorCode(UserErrorCodes.CreateUser.MissingUsername);

        RuleFor(c => c.ImageUrl)
            .NotEmpty().WithErrorCode(UserErrorCodes.CreateUser.MissingImageUrl);
    }
}
