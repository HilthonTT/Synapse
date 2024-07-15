namespace Modules.Posts.Application.Likes;

public static class LikeErrorCodes
{
    public static class CreateLike
    {
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingPostId = nameof(MissingPostId);
    }

    public static class Remove
    {
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingPostId = nameof(MissingPostId);
    }
}
