using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class SectionEntityConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(e => e.SectionId);

        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.SectionId)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(20);
    }
}