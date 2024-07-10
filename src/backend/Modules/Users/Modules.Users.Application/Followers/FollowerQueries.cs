using Dapper;
using System.Data;

namespace Modules.Users.Application.Followers;

public static class FollowerQueries
{
    public static async Task<List<FollowerResponse>> GetByUserId(
        IDbConnection connection, 
        Guid userId, 
        int limit = 10)
    {
        const string sql =
            """
            SELECT
                u.id As Id,
                u.name as Name
            FROM users.followers f
            JOIN users.users u ON u.id = f.user_id
            WHERE f.followed_id = @UserId
            ORDER BY created_on_utc DESC
            LIMIT @Limit
            """;

        IEnumerable<FollowerResponse> followers = await connection.QueryAsync<FollowerResponse>(
            sql,
            new
            {
                UserId = userId,
                Limit = limit
            });

        return followers.ToList();
    }

    public static async Task<FollowerStatsResponse> GetStatsAsync(
        IDbConnection connection, 
        Guid userId)
    {
        const string sql =
            """
            SELECT
                @UserId as UserId
                (
                    SELECT COUNT(*)
                    FROM users.followers f
                    WHERE f.followed_id = @UserId
                ) AS FollowerCount,
                (
                    SELECT COUNT(*)
                    FROM users.followers f
                    WHERE f.user_id = @UserId
                ) AS FollowingCount
            """;

        FollowerStatsResponse followerStats = await connection.QueryFirstAsync<FollowerStatsResponse>(
            sql,
            new { UserId = userId });

        return followerStats;
    }
}
