using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class BrandEntityConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(e => e.BrandId);

        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.BrandId)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(30);
    }
}
