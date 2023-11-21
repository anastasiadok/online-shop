using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class ProductVariantEntityConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasKey(e => e.ProductVariantId);

        builder.HasIndex(e => e.Sku).IsUnique();

        builder.Property(e => e.ProductVariantId)
            .ValueGeneratedNever();

        builder.Property(e => e.ColorId);

        builder.Property(e => e.ProductId);

        builder.Property(e => e.Quantity);

        builder.Property(e => e.SizeId);

        builder.Property(e => e.Sku)
            .HasMaxLength(255);

        builder.HasOne(d => d.Color).WithMany(p => p.ProductVariants)
            .HasForeignKey(d => d.ColorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Size).WithMany(p => p.ProductVariants)
            .HasForeignKey(d => d.SizeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}