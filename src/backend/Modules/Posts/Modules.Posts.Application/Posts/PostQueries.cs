using Dapper;
using Modules.Users.Api;
using System.Data;

namespace Modules.Posts.Application.Posts;

public static class PostQueries
{
    public static async Task<(List<PostResponse> Posts, Guid? NextCursor, Guid? PreviousCursor)> GetAsync(
        IDbConnection connection, 
        Guid? cursor, 
        int limit)
    {
        const string sql =
            """
            WITH CTE AS (
                SELECT
                    p.id AS Id,
                    p.title AS Title,
                    p.image_url AS ImageUrl,
                    p.tags AS Tags,
                    p.created_on_utc AS CreatedOnUtc,
                    p.location AS Location,
                    u.id AS UserId,
                    u.name AS Name,
                    u.username AS Username,
                    u.image_url AS UserImageUrl,
                    (
                        SELECT COUNT(*)
                        FROM posts.likes l
                        WHERE l.post_id = p.id
                    ) AS LikesCount,
                    (
                        SELECT COUNT(*)
                        FROM posts.comments c
                        WHERE c.post_id = p.id
                    ) AS CommentsCount,
                    ROW_NUMBER() OVER (ORDER BY p.created_on_utc DESC) AS RowNumber
                FROM posts.posts p
                LEFT JOIN users.users u ON u.id = p.user_id
                WHERE (@Cursor IS NULL OR p.created_on_utc < (SELECT created_on_utc FROM posts.posts WHERE id = @Cursor))
            )
            SELECT * FROM CTE
            WHERE RowNumber BETWEEN 1 AND @Limit + 1;
            """;

        IEnumerable<PostQueryResult> results = await connection.QueryAsync<PostQueryResult>(
            sql,
            new
            {
                Cursor = cursor,
                Limit = limit
            });

        List<PostResponse> posts = results
            .Take(limit)
            .Select(result =>
            {
                var userResponse = new UserResponse(
                    result.UserId,
                    result.UserName,
                    result.UserUsername,
                    result.UserImageUrl);

                return new PostResponse(
                    result.Id,
                    result.Title,
                    result.ImageUrl,
                    result.Tags,
                    result.Location,
                    userResponse,
                    result.LikesCount,
                    result.CommentsCount);
            })
            .ToList();

        Guid? nextCursor = posts.Count == limit ? posts.Last().Id : null;
        Guid? previousCursor = results.Any() ? results.First().Id : null;

        return (posts, nextCursor, previousCursor);
    }

    public static async Task<PostResponse?> GetByIdAsync(
        IDbConnection connection,
        Guid postId)
    {
        const string sql =
            """
            SELECT
                p.id AS Id,
                p.title AS Title,
                p.image_url AS ImageUrl,
                p.tags AS Tags,
                p.location AS Location,
                (
                    SELECT COUNT(*)
                    FROM posts.likes l
                    WHERE l.post_id = p.id
                ) AS LikesCount,
                (
                    SELECT COUNT(*)
                    FROM posts.comments c
                    WHERE c.post_id = p.id
                ) AS CommentsCount,
                u.id AS UserId,
                u.name AS UserName,
                u.username AS UserUsername,
                u.image_url AS UserImageUrl
            FROM posts.posts p
            LEFT JOIN users.users u ON u.id = p.user_id
            WHERE p.id = @PostId;
            """;

        PostQueryResult? result = await connection.QueryFirstOrDefaultAsync<PostQueryResult>(
            sql, 
            new { PostId = postId });

        if (result is null)
        {
            return null;
        }

        var userResponse = new UserResponse(
            result.UserId,
            result.UserName,
            result.UserUsername,
            result.UserImageUrl);

        return new PostResponse(
            result.Id,
            result.Title,
            result.ImageUrl,
            result.Tags,
            result.Location,
            userResponse,
            result.LikesCount,
            result.CommentsCount);
    }
}
