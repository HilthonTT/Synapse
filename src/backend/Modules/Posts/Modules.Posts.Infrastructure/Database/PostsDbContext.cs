using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Comments;
using Modules.Posts.Domain.Likes;
using Modules.Posts.Domain.Posts;

namespace Modules.Posts.Infrastructure.Database;

public sealed class PostsDbContext(DbContextOptions<PostsDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Post> Posts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostsDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());

        modelBuilder.HasDefaultSchema(Schemas.Posts);
    }
}
