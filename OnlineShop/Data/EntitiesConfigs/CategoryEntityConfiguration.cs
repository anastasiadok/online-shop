using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.EntitiesConfigs;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.CategoryId);

        builder.HasIndex(e => new { e.Name, e.SectionId }).IsUnique();

        builder.Property(e => e.CategoryId)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(20);

        builder.HasOne(d => d.Section).WithMany(p => p.Categories)
            .HasForeignKey(d => d.SectionId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.ParentCategory).WithMany(p => p.Categories)
            .HasForeignKey(d => d.ParentCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}