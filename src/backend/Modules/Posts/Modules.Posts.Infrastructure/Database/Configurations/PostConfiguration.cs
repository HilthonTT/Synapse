using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Posts.Domain.Posts;

namespace Modules.Posts.Infrastructure.Database.Configurations;

internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.Likes)
            .WithOne()
            .HasForeignKey(p => p.PostId)
            .IsRequired();

        builder.HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey(p => p.PostId)
            .IsRequired();
    }
}
