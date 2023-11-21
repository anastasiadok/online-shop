using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class MediaEntityConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(e => e.MediaId);

        builder.HasIndex(e => new { e.FileType, e.FileName }).IsUnique();

        builder.Property(e => e.MediaId)
            .ValueGeneratedNever();

        builder.Property(e => e.Bytes);

        builder.Property(e => e.FileName)
            .HasMaxLength(50);

        builder.Property(e => e.FileType)
            .HasMaxLength(10);

        builder.HasOne(d => d.Product).WithMany(p => p.Media)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}