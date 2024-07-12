using Dapper;
using System.Data;

namespace Modules.Posts.Application.Likes;

public static class LikeQueries
{
    public static async Task<List<LikeResponse>> GetByPostIdAsync(
        IDbConnection connection, 
        Guid postId)
    {
        const string sql =
            """
            SELECT
                l.post_id AS PostId,
                l.user_id AS UserId
            FROM posts.likes l
            WHERE l.post_id = @PostId;
            """;

        IEnumerable<LikeResponse> likes = await connection.QueryAsync<LikeResponse>(
            sql, 
            new { PostId = postId });

        return likes.ToList();
    }
}
