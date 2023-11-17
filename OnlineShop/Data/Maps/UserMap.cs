using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId).HasName("users_pkey");

        builder.ToTable("users");

        builder.HasIndex(e => new { e.Email, e.Phone }, "users_emai_phone_key").IsUnique();

        builder.Property(e => e.UserId)
            .ValueGeneratedNever()
            .HasColumnName("user_id");
        builder.Property(e => e.Role)
            .HasColumnType("user_type")
            .HasColumnName("role");
        builder.Property(e => e.Email)
            .HasMaxLength(40)
            .HasColumnName("email");
        builder.Property(e => e.FirstName)
            .HasMaxLength(30)
            .HasColumnName("first_name");
        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .HasColumnName("last_name");
        builder.Property(e => e.Password)
            .HasMaxLength(256)
            .HasColumnName("password");
        builder.Property(e => e.Phone)
            .HasMaxLength(20)
            .HasColumnName("phone");
    }
}