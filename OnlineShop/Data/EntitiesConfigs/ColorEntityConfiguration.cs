using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class ColorEntityConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasKey(e => e.ColorId);

        builder.HasIndex(e => e.ColorName).IsUnique();

        builder.Property(e => e.ColorId)
            .ValueGeneratedNever();

        builder.Property(e => e.ColorName)
            .HasMaxLength(20);
    }
}