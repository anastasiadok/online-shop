using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(e => e.AddressId).HasName("adresses_pkey");

        builder.ToTable("addresses");

        builder.Property(e => e.AddressId)
            .ValueGeneratedNever()
            .HasColumnName("address_id");

        builder.Property(e => e.Address1)
                    .HasMaxLength(100)
                    .HasColumnName("address");

        builder.HasOne(d => d.User).WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adresses_user_id_fkey");
    }
}
