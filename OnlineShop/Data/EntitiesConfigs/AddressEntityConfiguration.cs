using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(e => e.AddressId);

        builder.Property(e => e.AddressId)
            .ValueGeneratedNever();

        builder.Property(e => e.Address1)
                    .HasMaxLength(100);

        builder.HasOne(d => d.User).WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
