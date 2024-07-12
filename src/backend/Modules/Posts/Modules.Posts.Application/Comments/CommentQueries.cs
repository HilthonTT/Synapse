using Dapper;
using Modules.Users.Api;
using System.Data;

namespace Modules.Posts.Application.Comments;

public static class CommentQueries
{
    public static async Task<List<CommentResponse>> GetByPostIdAsync(
        IDbConnection connection, 
        Guid postId)
    {
        const string sql =
            """
            SELECT
                c.id AS Id,
                c.post_id AS PostId,
                u.id AS UserId,
                u.name AS Name,
                u.username AS Username,
                u.image_url AS ImageUrl,
                c.created_on_utc AS CreatedOnUtc,
                c.modified_on_utc AS ModifiedOnUtc
            FROM posts.comments c
            LEFT JOIN users.users u ON u.id = c.user_id
            WHERE c.post_id = @PostId;
            """;

        IEnumerable<CommentResponse> comments = await connection.QueryAsync<CommentResponse, UserResponse, CommentResponse>(
            sql,
            (comment, user) =>
            {
                return new CommentResponse(
                    comment.Id,
                    comment.PostId,
                    user,
                    comment.CreatedOnUtc,
                    comment.ModifiedOnUtc
                );
            },
            new { PostId = postId },
            splitOn: "UserId");

        return comments.ToList();
    }
}
