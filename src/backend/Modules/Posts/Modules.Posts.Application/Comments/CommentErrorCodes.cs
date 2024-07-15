namespace Modules.Posts.Application.Comments;

public static class CommentErrorCodes
{
    public static class CreateComment
    {
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingPostId = nameof(MissingPostId);
        public const string MissingContent = nameof(MissingContent);
        public const string ContentTooLong = nameof(ContentTooLong);
    }

    public static class UpdateComment
    {
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingCommentId = nameof(MissingCommentId);
        public const string MissingContent = nameof(MissingContent);
        public const string ContentTooLong = nameof(ContentTooLong);
    }

    public static class RemoveComment
    {
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingCommentId = nameof(MissingCommentId);
    }
}
