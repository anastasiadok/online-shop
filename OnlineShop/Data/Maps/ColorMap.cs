using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class ColorMap : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasKey(e => e.ColorId).HasName("colors_pkey");

        builder.ToTable("colors");

        builder.HasIndex(e => e.ColorName, "colors_color_name_key").IsUnique();

        builder.Property(e => e.ColorId)
            .ValueGeneratedNever()
            .HasColumnName("color_id");
        builder.Property(e => e.ColorName)
            .HasMaxLength(20)
            .HasColumnName("color_name");
    }
}