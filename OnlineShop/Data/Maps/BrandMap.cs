using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class BrandMap : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(e => e.BrandId).HasName("brands_pkey");

        builder.ToTable("brands");

        builder.HasIndex(e => e.Name, "brands_name_key").IsUnique();

        builder.Property(e => e.BrandId)
            .ValueGeneratedNever()
            .HasColumnName("brand_id");
        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .HasColumnName("name");
    }
}
