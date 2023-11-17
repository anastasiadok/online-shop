using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => new { e.ProductVariantId, e.OrderId }).HasName("pk_order_item");

        builder.ToTable("order_items");

        builder.Property(e => e.ProductVariantId).HasColumnName("product_var_id");
        builder.Property(e => e.OrderId).HasColumnName("order_id");
        builder.Property(e => e.Quantity).HasColumnName("quantity");

        builder.HasOne(d => d.Order).WithMany(p => p.OrderItems)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("order_items_order_id_fkey");

        builder.HasOne(d => d.ProductVariant).WithMany(p => p.OrderItems)
            .HasForeignKey(d => d.ProductVariantId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("order_items_product_var_id_fkey");
    }
}