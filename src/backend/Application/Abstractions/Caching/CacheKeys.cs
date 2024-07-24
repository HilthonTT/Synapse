namespace Application.Abstractions.Caching;

public static class CacheKeys
{
    public static class Posts
    {
        public const string SearchPrefix = "posts-search";
        public const string CursorPrefix = "posts-cursor";
        public const string UserPrefix = "posts-user";

        public static string Cursor(Guid? cursor, int limit) =>
            $"{CursorPrefix}-{cursor}-limit-{limit}";

        public static string Id(Guid postId) =>
            $"posts-{postId}";

        public static string UserId(Guid userId) =>
            $"{UserPrefix}-{userId}";

        public static string Search(string? searchTerm, int sortOrder, int sortColumn) =>
            $"{SearchPrefix}-{searchTerm}-order-{sortOrder}-column-{sortColumn}";
    }

    public static class Users
    {
        public const string UserList = "users";

        public static string Id(Guid userId) =>
            $"users-{userId}";
    }

    public static class Followers
    {
        public static string UserId(Guid userId) =>
            $"follower-stats-user-{userId}";

        public static string RecentUserId(Guid userId) =>
            $"follower-recent-user-{userId}";
    }

    public static class Likes
    {
        public static string PostId(Guid postId) =>
            $"posts-likes-{postId}";
    }

    public static class Comments
    {
        public static string PostId(Guid postId) =>
            $"comments-post-{postId}";
    }
}
