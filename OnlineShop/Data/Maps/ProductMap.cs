using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.ProductId).HasName("products_pkey");

        builder.ToTable("products");

        builder.HasIndex(e => new { e.BrandId, e.CategoryId }, "prod_by_brand_category");

        builder.HasIndex(e => e.Name, "products_name_key").IsUnique();

        builder.Property(e => e.ProductId)
            .ValueGeneratedNever()
            .HasColumnName("product_id");
        builder.Property(e => e.AverageRating).HasColumnName("average_rating");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.CategoryId).HasColumnName("category_id");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
        builder.Property(e => e.Price)
            .HasColumnType("money")
            .HasColumnName("price");

        builder.HasOne(d => d.Brand).WithMany(p => p.Products)
            .HasForeignKey(d => d.BrandId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("products_brand_id_fkey");

        builder.HasOne(d => d.Category).WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("products_category_id_fkey");
    }
}