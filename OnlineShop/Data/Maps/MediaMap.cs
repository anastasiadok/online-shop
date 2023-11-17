using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class MediaMap : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(e => e.MediaId).HasName("media_pkey");

        builder.ToTable("media");

        builder.HasIndex(e => new { e.FileType, e.FileName }, "media_file_type_file_name_key").IsUnique();

        builder.Property(e => e.MediaId)
            .ValueGeneratedNever()
            .HasColumnName("medium_id");
        builder.Property(e => e.Bytes).HasColumnName("bytes");
        builder.Property(e => e.FileName)
            .HasMaxLength(50)
            .HasColumnName("file_name");
        builder.Property(e => e.FileType)
            .HasMaxLength(10)
            .HasColumnName("file_type");

        builder.HasOne(d => d.Product).WithMany(p => p.Media)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("media_product_id_fkey");
    }
}