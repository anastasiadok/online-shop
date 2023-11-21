using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(e => e.ReviewId);

        builder.HasIndex(e => new { e.ProductId, e.Rating });

        builder.Property(e => e.ReviewId)
            .ValueGeneratedNever();

        builder.Property(e => e.CommentText)
            .HasMaxLength(2000);

        builder.Property(e => e.CreatedAt);

        builder.Property(e => e.ProductId);

        builder.Property(e => e.Rating);

        builder.Property(e => e.Title)
            .HasMaxLength(50);

        builder.Property(e => e.UserId);

        builder.HasOne(d => d.Product).WithMany(p => p.Reviews)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.User).WithMany(p => p.Reviews)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}