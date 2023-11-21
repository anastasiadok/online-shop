using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class SizeEntityConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.HasKey(e => e.SizeId);

        builder.HasIndex(e => e.SizeName).IsUnique();

        builder.Property(e => e.SizeId)
            .ValueGeneratedNever();

        builder.Property(e => e.SizeName)
            .HasMaxLength(10);
    }
}