using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.ProductId);

        builder.HasIndex(e => new { e.BrandId, e.CategoryId });

        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.ProductId)
            .ValueGeneratedNever();

        builder.Property(e => e.AverageRating);

        builder.Property(e => e.BrandId);

        builder.Property(e => e.CategoryId);

        builder.Property(e => e.Name)
            .HasMaxLength(50);

        builder.Property(e => e.Price);

        builder.HasOne(d => d.Brand).WithMany(p => p.Products)
            .HasForeignKey(d => d.BrandId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Category).WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}