using Dapper;
using Modules.Posts.Application.Comments.GetByPostId;
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
                c.content AS Content,
                u.id AS UserId,
                u.name AS Name,
                u.username AS Username,
                u.image_url AS ImageUrl,
                c.created_on_utc AS CreatedOnUtc,
                c.modified_on_utc AS ModifiedOnUtc
            FROM posts.comments c
            LEFT JOIN users.users u ON u.id = c.user_id
            WHERE c.post_id = @PostId
            ORDER BY c.created_on_utc ASC;
            """;

        IEnumerable<CommentQueryResult> results = await connection.QueryAsync<CommentQueryResult>(
            sql, 
            new { PostId = postId });

        List<CommentResponse> comments = results.Select(result =>
        {
            var userResponse = new UserApiResponse(
                result.UserId,
                result.Name,
                result.Username,
                result.ImageUrl
            );

            return new CommentResponse(
                result.Id,
                result.PostId,
                result.Content,
                userResponse,
                result.CreatedOnUtc,
                result.ModifiedOnUtc
            );
        }).ToList();

        return comments;
    }
}
