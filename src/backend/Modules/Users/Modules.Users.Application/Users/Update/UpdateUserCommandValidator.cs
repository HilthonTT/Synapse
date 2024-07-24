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
            .NotEmpty().WithErrorCode(UserErrorCodes.UpdateUser.MissingImageUrl)
            .Must(BeAValidUrl).WithErrorCode(UserErrorCodes.CreateUser.InvalidImageUrl);
    }

    private static bool BeAValidUrl(string imageUrl)
    {
        return Uri.TryCreate(imageUrl, UriKind.Absolute, out Uri? uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
