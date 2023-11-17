using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class SectionMap : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(e => e.SectionId).HasName("sections_pkey");

        builder.ToTable("sections");

        builder.HasIndex(e => e.Name, "sections_name_key").IsUnique();

        builder.Property(e => e.SectionId)
            .ValueGeneratedNever()
            .HasColumnName("section_id");
        builder.Property(e => e.Name)
            .HasMaxLength(20)
            .HasColumnName("name");
    }
}