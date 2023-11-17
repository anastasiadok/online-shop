using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class CartItemMap : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(e => new { e.UserId, e.ProductVariantId }).HasName("pk_cart_item");

        builder.ToTable("cart_items");

        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.ProductVariantId).HasColumnName("product_var_id");
        builder.Property(e => e.Quantity).HasColumnName("quantity");

        builder.HasOne(d => d.ProductVariant).WithMany(p => p.CartItems)
            .HasForeignKey(d => d.ProductVariantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("cart_items_product_var_id_fkey");

        builder.HasOne(d => d.User).WithMany(p => p.CartItems)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("cart_items_user_id_fkey");
    }
}