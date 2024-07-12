using Dapper;
using Modules.Users.Api;
using System.Data;

namespace Modules.Posts.Application.Posts;

public static class PostQueries
{
    public static async Task<List<PostResponse>> GetAsync(IDbConnection connection)
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
                u.image_url AS ImageUrl,
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
            LEFT JOIN users.users u ON u.id = p.user_id;
            """;

        IEnumerable<PostResponse> posts = await connection.QueryAsync<PostResponse, UserResponse, PostResponse>(
            sql,
            (post, user) =>
            {
                return new PostResponse(
                    post.Id,
                    post.Title,
                    post.ImageUrl,
                    post.Tags,
                    user,
                    post.LikesCount,
                    post.CommentsCount
                );
            },
            splitOn: "UserId");

        return posts.ToList();
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
                u.id AS UserId,
                u.name AS Name,
                u.username AS Username,
                u.image_url AS ImageUrl,
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
            WHERE p.id = @PostId;
            """;

        IEnumerable<PostResponse> posts = await connection.QueryAsync<PostResponse, UserResponse, PostResponse>(
            sql,
            (post, user) =>
            {
                return new PostResponse(
                    post.Id,
                    post.Title,
                    post.ImageUrl,
                    post.Tags,
                    user,
                    post.LikesCount,
                    post.CommentsCount
                );
            },
            new { PostId = postId },
            splitOn: "UserId");

        return posts.FirstOrDefault();
    }
}
