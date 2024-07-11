using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Posts.Domain.Likes;
using Modules.Posts.Domain.Posts;

namespace Modules.Posts.Infrastructure.Database.Configurations;

internal sealed class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(l => new { l.PostId, l.UserId });

        builder.HasIndex(l => new { l.PostId, l.UserId });

        builder.HasOne<Post>()
            .WithMany()
            .HasForeignKey(f => f.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(l => l.Id);
    }
}
