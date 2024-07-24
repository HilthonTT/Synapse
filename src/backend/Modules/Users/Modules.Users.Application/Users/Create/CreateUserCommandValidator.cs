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
            .NotEmpty().WithErrorCode(UserErrorCodes.CreateUser.MissingImageUrl)
            .Must(BeAValidUrl).WithErrorCode(UserErrorCodes.CreateUser.InvalidImageUrl);
    }

    private static bool BeAValidUrl(string imageUrl)
    {
        return Uri.TryCreate(imageUrl, UriKind.Absolute, out Uri? uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
