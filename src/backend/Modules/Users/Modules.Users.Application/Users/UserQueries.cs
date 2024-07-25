using System.Data;
using System.Text;
using Dapper;

namespace Modules.Users.Application.Users;

public static class UserQueries
{
    public static async Task<List<UserResponse>> GetAsync(
        IDbConnection connection, 
        int? limit)
    {
        StringBuilder sql = new(
            """
            SELECT
                u.id AS Id,
                u.name AS Name,
                u.username AS Username,
                u.image_url AS ImageUrl,
                u.created_on_utc AS CreatedOnUtc,
                u.modified_on_utc AS ModifiedOnUtc
            FROM users.users u
            """
        );

        if (limit.HasValue)
        {
            sql.Append(" LIMIT @Limit");
        }

        IEnumerable<UserResponse> users = await connection.QueryAsync<UserResponse>(
            sql.ToString(),
            new { Limit = limit });

        return users.ToList();
    }

    public static async Task<UserResponse?> GetByIdAsync(
        IDbConnection connection,
        Guid userId)
    {
        const string sql =
            """
            SELECT
                u.id AS Id,
                u.name AS Name,
                u.username AS Username,
                u.image_url AS ImageUrl,
                u.created_on_utc AS CreatedOnUtc,
                u.modified_on_utc AS ModifiedOnUtc
            FROM users.users u
            WHERE u.id = @UserId
            """;

        UserResponse? user = await connection.QueryFirstOrDefaultAsync<UserResponse>(
            sql,
            new { UserId = userId });

        return user;
    }
}
