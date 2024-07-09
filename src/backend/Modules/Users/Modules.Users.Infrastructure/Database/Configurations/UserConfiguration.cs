using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Users;

namespace Modules.Users.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ComplexProperty(
            u => u.ObjectIdentifier,
            b => b.Property(e => e.Value).HasColumnName("object_identifier"));

        builder.ComplexProperty(
            u => u.Username,
            b => b.Property(e => e.Value).HasColumnName("username"));

        builder.ComplexProperty(
            u => u.Email,
            b => b.Property(e => e.Value).HasColumnName("email"));

        builder.ComplexProperty(
            u => u.Name,
            b => b.Property(e => e.Value).HasColumnName("name"));

        builder.HasIndex(u => u.Email.Value).IsUnique();

        builder.HasIndex(u => u.ObjectIdentifier.Value).IsUnique();
    }
}
