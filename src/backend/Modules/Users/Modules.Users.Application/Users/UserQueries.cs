using System.Data;
using Dapper;

namespace Modules.Users.Application.Users;

public static class UserQueries
{
    public static async Task<List<UserResponse>> GetAsync(IDbConnection connection)
    {
        const string sql =
            """
            SELECT
                u.id as Id,
                u.name as Name,
                u.username as Username,
                u.created_on_utc as CreatedOnUtc,
                u.modified_on_utc as ModifiedOnUtc
            FROM users.users u
            """;

        IEnumerable<UserResponse> users = await connection.QueryAsync<UserResponse>(sql);

        return users.ToList();
    }

    public static async Task<UserResponse?> GetByIdAsync(
        IDbConnection connection, 
        Guid userId)
    {
        const string sql =
            """
            SELECT
                u.id as Id,
                u.name as Name,
                u.username as Username,
                u.created_on_utc as CreatedOnUtc,
                u.modified_on_utc as ModifiedOnUtc
            FROM users.users u
            WHERE u.id = @UserId
            """;

        UserResponse? user = await connection.QueryFirstOrDefaultAsync<UserResponse>(
            sql, 
            new { UserId = userId });

        return user;
    }
}
