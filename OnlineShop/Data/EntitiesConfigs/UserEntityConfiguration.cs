using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId);

        builder.HasIndex(e => new { e.Email, e.Phone }).IsUnique();

        builder.Property(e => e.UserId)
            .ValueGeneratedNever();

        builder.Property(e => e.Role);

        builder.Property(e => e.Email)
            .HasMaxLength(40);

        builder.Property(e => e.FirstName)
            .HasMaxLength(30);

        builder.Property(e => e.LastName)
            .HasMaxLength(50);

        builder.Property(e => e.Password)
            .HasMaxLength(255);

        builder.Property(e => e.Phone)
            .HasMaxLength(20);
    }
}