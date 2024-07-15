namespace Modules.Users.Application.Users;

public static class UserErrorCodes
{
    public static class CreateUser
    {
        public const string MissingObjectIdentifier = nameof(MissingObjectIdentifier);
        public const string MissingEmail = nameof(MissingEmail);
        public const string MissingName = nameof(MissingName);
        public const string MissingImageUrl = nameof(MissingImageUrl);
        public const string InvalidEmail = nameof(InvalidEmail);
    }

    public static class UpdateUser
    {
        public const string MissingId = nameof(MissingId);
        public const string MissingEmail = nameof(MissingEmail);
        public const string MissingName = nameof(MissingName);
        public const string MissingImageUrl = nameof(MissingImageUrl);
        public const string InvalidEmail = nameof(InvalidEmail);
    }
}
