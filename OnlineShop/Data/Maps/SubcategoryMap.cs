using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Maps;

//public class SubcategoryMap : IEntityTypeConfiguration<Subcategory>
//{
//    public void Configure(EntityTypeBuilder<Subcategory> builder)
//    {
//        builder.HasKey(e => e.SubcategoryId).HasName("subcategories_pkey");

//        builder.ToTable("subcategories");

//        builder.HasIndex(e => new { e.Name, e.CategoryId }, "subcategories_name_categories_key").IsUnique();

//        builder.Property(e => e.SubcategoryId)
//            .ValueGeneratedNever()
//            .HasColumnName("subcategory_id");
//        builder.Property(e => e.Name)
//            .HasMaxLength(20)
//            .HasColumnName("name");

//        builder.HasOne(d => d.Category).WithMany(p => p.Subcategories)
//            .HasForeignKey(d => d.CategoryId)
//            .OnDelete(DeleteBehavior.ClientSetNull)
//            .HasConstraintName("category_subcategories_id_fkey");
//    }
//}