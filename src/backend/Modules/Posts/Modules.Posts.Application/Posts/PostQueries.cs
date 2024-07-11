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
                p.
            """;

        return null;
    }
}
