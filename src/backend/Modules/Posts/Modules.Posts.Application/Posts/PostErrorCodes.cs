namespace Modules.Posts.Application.Posts;

public static class PostErrorCodes
{
    public static class CreatePost
    {
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingTitle = nameof(MissingTitle);
        public const string MissingImageUrl = nameof(MissingImageUrl);
        public const string TitleTooLong = nameof(TitleTooLong);
        public const string InvalidImageUrl = nameof(InvalidImageUrl);
    }

    public static class UpdatePost
    {
        public const string MissingPostId = nameof(MissingPostId);
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingTitle = nameof(MissingTitle);
        public const string MissingImageUrl = nameof(MissingImageUrl);
        public const string TitleTooLong = nameof(TitleTooLong);
        public const string InvalidImageUrl = nameof(InvalidImageUrl);
    }

    public static class RemovePost
    {
        public const string MissingPostId = nameof(MissingPostId);
        public const string MissingUserId = nameof(MissingUserId);
    }

    public static class SearchPosts
    {
        public const string IncorrectSortOrder = nameof(IncorrectSortOrder);
        public const string IncorrectSortColumn = nameof(IncorrectSortColumn);
    }
}
