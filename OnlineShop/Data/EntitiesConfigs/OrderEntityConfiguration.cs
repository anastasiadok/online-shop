using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.OrderId);

        builder.ToTable("orders");

        builder.Property(e => e.OrderId)
            .ValueGeneratedNever();

        builder.Property(e => e.AddressId);

        builder.Property(e => e.CreatedAt);

        builder.Property(e => e.TotalPrice);

        builder.Property(e => e.UserId);

        builder.Property(e => e.Status);

        builder.HasOne(d => d.Address).WithMany(p => p.Orders)
            .HasForeignKey(d => d.AddressId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.User).WithMany(p => p.Orders)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}