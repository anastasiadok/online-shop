using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class ProductVariantMap : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasKey(e => e.ProductVariantId).HasName("product_variants_pkey");

        builder.ToTable("product_variants");

        builder.HasIndex(e => e.Sku, "product_variants_sku_key").IsUnique();

        builder.Property(e => e.ProductVariantId)
            .ValueGeneratedNever()
            .HasColumnName("prod_variant_id");
        builder.Property(e => e.ColorId).HasColumnName("color_id");
        builder.Property(e => e.ProductId).HasColumnName("product_id");
        builder.Property(e => e.Quantity).HasColumnName("quantity");
        builder.Property(e => e.SizeId).HasColumnName("size_id");
        builder.Property(e => e.Sku)
            .HasMaxLength(256)
            .HasColumnName("sku");

        builder.HasOne(d => d.Color).WithMany(p => p.ProductVariants)
            .HasForeignKey(d => d.ColorId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("product_variants_color_id_fkey");

        builder.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("product_variants_product_id_fkey");

        builder.HasOne(d => d.Size).WithMany(p => p.ProductVariants)
            .HasForeignKey(d => d.SizeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("product_variants_size_id_fkey");
    }
}