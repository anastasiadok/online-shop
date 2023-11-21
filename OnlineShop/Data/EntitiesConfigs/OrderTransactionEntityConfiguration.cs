using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class OrderTransactionEntityConfiguration : IEntityTypeConfiguration<OrderTransaction>
{
    public void Configure(EntityTypeBuilder<OrderTransaction> builder)
    {
        builder.HasKey(e => new { e.OrderId, e.Status });

        builder.HasIndex(e => new { e.OrderId, e.Status }).IsUnique();

        builder.Property(e => e.OrderId);

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.Status);

        builder.HasOne(d => d.Order).WithMany()
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}