using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Users;

namespace Modules.Users.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        // Configure ObjectIdentifier as a value object
        builder.OwnsOne(
            u => u.ObjectIdentifier,
            b =>
            {
                b.Property(e => e.Value)
                 .HasColumnName("object_identifier")
                 .IsRequired();
                b.HasIndex(e => e.Value).IsUnique();
            });

        // Configure Username as a value object
        builder.OwnsOne(
            u => u.Username,
            b =>
            {
                b.Property(e => e.Value)
                 .HasColumnName("username")
                 .IsRequired();
            });

        // Configure Email as a value object
        builder.OwnsOne(
            u => u.Email,
            b =>
            {
                b.Property(e => e.Value)
                 .HasColumnName("email")
                 .IsRequired();
                b.HasIndex(e => e.Value).IsUnique();
            });

        // Configure Name as a value object
        builder.OwnsOne(
            u => u.Name,
            b =>
            {
                b.Property(e => e.Value)
                 .HasColumnName("name")
                 .IsRequired();
            });
    }
}
