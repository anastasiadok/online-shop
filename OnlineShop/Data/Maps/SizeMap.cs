using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class SizeMap : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.HasKey(e => e.SizeId).HasName("sizes_pkey");

        builder.ToTable("sizes");

        builder.HasIndex(e => e.SizeName, "sizes_size_name_key").IsUnique();

        builder.Property(e => e.SizeId)
            .ValueGeneratedNever()
            .HasColumnName("size_id");

        builder.Property(e => e.SizeName)
            .HasMaxLength(10)
            .HasColumnName("size_name");
    }
}