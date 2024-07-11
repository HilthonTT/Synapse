using Microsoft.EntityFrameworkCore;
using Modules.Posts.Infrastructure.Database;
using Modules.Users.Infrastructure.Database;

namespace Web.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigration(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using UsersDbContext usersContext = 
            scope.ServiceProvider.GetRequiredService<UsersDbContext>();

        usersContext.Database.Migrate();

        using PostsDbContext postsContext =
            scope.ServiceProvider.GetRequiredService<PostsDbContext>();

        postsContext.Database.Migrate();
    }
}
