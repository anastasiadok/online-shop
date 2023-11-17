using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.OrderId).HasName("orders_pkey");

        builder.ToTable("orders");

        builder.Property(e => e.OrderId)
            .ValueGeneratedNever()
            .HasColumnName("order_id");
        builder.Property(e => e.AddressId).HasColumnName("address_id");
        builder.Property(e => e.CreatedAt)
            .HasPrecision(6)
            .HasColumnName("created_at");
        builder.Property(e => e.TotalPrice)
            .HasColumnType("money")
            .HasColumnName("total_price");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.Status)
            .HasColumnType("status_type")
            .HasColumnName("status");

        builder.HasOne(d => d.Address).WithMany(p => p.Orders)
            .HasForeignKey(d => d.AddressId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("orders_adress_id_fkey");

        builder.HasOne(d => d.User).WithMany(p => p.Orders)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("orders_user_id_fkey");
    }
}