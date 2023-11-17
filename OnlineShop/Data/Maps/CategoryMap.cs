using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.CategoryId).HasName("categories_pkey");

        builder.ToTable("categories");

        builder.HasIndex(e => new { e.Name, e.SectionId }, "categories_name_sections_key").IsUnique();

        builder.Property(e => e.CategoryId)
            .ValueGeneratedNever()
            .HasColumnName("category_id");
        builder.Property(e => e.Name)
            .HasMaxLength(20)
            .HasColumnName("name");

        builder.HasOne(d => d.Section).WithMany(p => p.Categories)
            .HasForeignKey(d => d.SectionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("section_categories_id_fkey");

        builder.HasOne(d => d.ParentCategory).WithMany(p => p.Categories)
            .HasForeignKey(d => d.ParentCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("parentcategory_categories_id_fkey");
    }
}