using Dapper;
using Modules.Users.Api;
using System.Data;

namespace Modules.Posts.Application.Posts;

public static class PostQueries
{
    public static async Task<List<PostResponse>> GetAsync(IDbConnection connection, Guid? cursor, int limit)
    {
        const string sql =
            """
            SELECT
                p.id AS Id,
                p.title AS Title,
                p.image_url AS ImageUrl,
                p.tags AS Tags,
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
                ) AS CommentsCount
            FROM posts.posts p
            LEFT JOIN users.users u ON u.id = p.user_id
            WHERE (@Cursor IS NULL OR p.id < @Cursor)
            ORDER BY p.id DESC
            LIMIT @Limit;
            """;

        IEnumerable<PostQueryResult> results = await connection.QueryAsync<PostQueryResult>(
            sql, 
            new 
            { 
                Cursor = cursor, 
                Limit = limit 
            });

        List<PostResponse> posts = results.Select(result =>
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
                userResponse,
                result.LikesCount,
                result.CommentsCount);
        }).ToList();

        return posts;
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

        PostQueryResult? postResult = await connection.QueryFirstOrDefaultAsync<PostQueryResult>(
            sql, 
            new { PostId = postId });
        if (postResult is null)
        {
            return null;
        }

        var userResponse = new UserResponse(
            postResult.UserId,
            postResult.UserName,
            postResult.UserUsername,
            postResult.UserImageUrl);

        return new PostResponse(
            postResult.Id,
            postResult.Title,
            postResult.ImageUrl,
            postResult.Tags,
            userResponse,
            postResult.LikesCount,
            postResult.CommentsCount);
    }
}
