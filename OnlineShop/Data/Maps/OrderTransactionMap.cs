using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class OrderTransactionMap : IEntityTypeConfiguration<OrderTransaction>
{
    public void Configure(EntityTypeBuilder<OrderTransaction> builder)
    {
        builder.HasKey(e => new { e.OrderId, e.Status }).HasName("pk_order_tr");
        builder.ToTable("order_transactions");

        builder.HasIndex(e => new { e.OrderId, e.Status }, "by_order_status").IsUnique();

        builder.Property(e => e.OrderId).HasColumnName("order_id");
        builder.Property(e => e.UpdatedAt)
            .HasPrecision(6)
            .HasColumnName("updated_at");
        builder.Property(e => e.Status)
            .HasColumnType("status_type")
            .HasColumnName("status");

        builder.HasOne(d => d.Order).WithMany()
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("order_transactions_transaction_id_fkey");
    }
}