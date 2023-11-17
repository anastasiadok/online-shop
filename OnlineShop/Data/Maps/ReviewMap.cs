using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class ReviewMap : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(e => e.ReviewId).HasName("reviews_pkey");

        builder.ToTable("reviews");

        builder.HasIndex(e => new { e.ProductId, e.Rating }, "by_product_rating");

        builder.Property(e => e.ReviewId)
            .ValueGeneratedNever()
            .HasColumnName("review_id");
        builder.Property(e => e.CommentText)
            .HasMaxLength(2000)
            .HasColumnName("comment_text");
        builder.Property(e => e.CreatedAt)
            .HasPrecision(6)
            .HasColumnName("created_at");
        builder.Property(e => e.ProductId).HasColumnName("product_id");
        builder.Property(e => e.Rating).HasColumnName("rating");
        builder.Property(e => e.Title)
            .HasMaxLength(50)
            .HasColumnName("title");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.Product).WithMany(p => p.Reviews)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("reviews_product_id_fkey");

        builder.HasOne(d => d.User).WithMany(p => p.Reviews)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("reviews_user_id_fkey");
    }
}