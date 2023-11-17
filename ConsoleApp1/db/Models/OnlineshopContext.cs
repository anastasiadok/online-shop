using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ConsoleApp1.db.Models;

public partial class OnlineshopContext : DbContext
{
    static OnlineshopContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionStatus>("status_type");
        NpgsqlConnection.GlobalTypeMapper.MapEnum<UserType>("user_type");
    }

    public OnlineshopContext()
    {
        
    }
    public OnlineshopContext(DbContextOptions<OnlineshopContext> options)
        : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Brand> Brands { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Color> Colors { get; set; }

    public DbSet<Media> Media { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<OrderTransaction> OrderTransactions { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductVariant> ProductVariants { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Section> Sections { get; set; }

    public DbSet<Size> Sizes { get; set; }

    public DbSet<Subcategory> Subcategories { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=onlineshop;Username=postgres;Password=nastya11*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("status_type", new[] { "in_review", "in_delivery", "completed", "cancelled" })
            .HasPostgresEnum("user_type", new[] { "admin", "user" });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("adresses_pkey");

            entity.ToTable("addresses");

            entity.Property(e => e.AddressId)
                .ValueGeneratedNever()
                .HasColumnName("address_id");
            entity.Property(e => e.Address1)
                .HasMaxLength(100)
                .HasColumnName("address");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("adresses_user_id_fkey");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("brands_pkey");

            entity.ToTable("brands");

            entity.HasIndex(e => e.Name, "brands_name_key").IsUnique();

            entity.Property(e => e.BrandId)
                .ValueGeneratedNever()
                .HasColumnName("brand_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductVariantId }).HasName("pk_cart_item");

            entity.ToTable("cart_items");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ProductVariantId).HasColumnName("product_var_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_items_product_var_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_items_user_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => new { e.Name, e.SectionId }, "categories_name_sections_key").IsUnique();

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");

            entity.HasOne(d => d.Section).WithMany(p => p.Categories)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_categories_id_fkey");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("colors_pkey");

            entity.ToTable("colors");

            entity.HasIndex(e => e.ColorName, "colors_color_name_key").IsUnique();

            entity.Property(e => e.ColorId)
                .ValueGeneratedNever()
                .HasColumnName("color_id");
            entity.Property(e => e.ColorName)
                .HasMaxLength(20)
                .HasColumnName("color_name");
        });

        modelBuilder.Entity<Media>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("media_pkey");

            entity.ToTable("media");

            entity.HasIndex(e => new { e.FileType, e.FileName }, "media_file_type_file_name_key").IsUnique();

            entity.Property(e => e.MediaId)
                .ValueGeneratedNever()
                .HasColumnName("medium_id");
            entity.Property(e => e.Bytes).HasColumnName("bytes");
            entity.Property(e => e.FileName)
                .HasMaxLength(50)
                .HasColumnName("file_name");
            entity.Property(e => e.FileType)
                .HasMaxLength(10)
                .HasColumnName("file_type");
            
            entity.HasOne(d => d.Product).WithMany(p => p.Media)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("media_product_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("money")
                .HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Status)
                .HasColumnType("status_type")
                .HasColumnName("status");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_adress_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_user_id_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.ProductVariantId, e.OrderId }).HasName("pk_order_item");

            entity.ToTable("order_items");

            entity.Property(e => e.ProductVariantId).HasColumnName("product_var_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_order_id_fkey");

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_product_var_id_fkey");
        });

        modelBuilder.Entity<OrderTransaction>(entity =>
        {
            entity.HasKey(e => new{ e.OrderId,e.Status}).HasName("pk_order_tr");
            entity.ToTable("order_transactions");

            entity.HasIndex(e => new { e.OrderId, e.Status }, "by_order_status").IsUnique();

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("updated_at");
            entity.Property(e => e.Status)
                .HasColumnType("status_type")
                .HasColumnName("status");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_transactions_transaction_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => new { e.BrandId, e.SubcategoryId }, "prod_by_brand_subcategory");

            entity.HasIndex(e => e.Name, "products_name_key").IsUnique();

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("product_id");
            entity.Property(e => e.AverageRating).HasColumnName("average_rating");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_brand_id_fkey");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubcategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_subcategory_id_fkey");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.ProductVariantId).HasName("product_variants_pkey");

            entity.ToTable("product_variants");

            entity.HasIndex(e => e.Sku, "product_variants_sku_key").IsUnique();

            entity.Property(e => e.ProductVariantId)
                .ValueGeneratedNever()
                .HasColumnName("prod_variant_id");
            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.Sku)
                .HasMaxLength(256)
                .HasColumnName("sku");

            entity.HasOne(d => d.Color).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_variants_color_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_variants_product_id_fkey");

            entity.HasOne(d => d.Size).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_variants_size_id_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("reviews_pkey");

            entity.ToTable("reviews");

            entity.HasIndex(e => new { e.ProductId, e.Rating }, "by_product_rating");

            entity.Property(e => e.ReviewId)
                .ValueGeneratedNever()
                .HasColumnName("review_id");
            entity.Property(e => e.CommentText)
                .HasMaxLength(2000)
                .HasColumnName("comment_text");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_product_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_user_id_fkey");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("sections_pkey");

            entity.ToTable("sections");

            entity.HasIndex(e => e.Name, "sections_name_key").IsUnique();

            entity.Property(e => e.SectionId)
                .ValueGeneratedNever()
                .HasColumnName("section_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("sizes_pkey");

            entity.ToTable("sizes");

            entity.HasIndex(e => e.SizeName, "sizes_size_name_key").IsUnique();

            entity.Property(e => e.SizeId)
                .ValueGeneratedNever()
                .HasColumnName("size_id");
            entity.Property(e => e.SizeName)
                .HasMaxLength(10)
                .HasColumnName("size_name");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.SubcategoryId).HasName("subcategories_pkey");

            entity.ToTable("subcategories");

            entity.HasIndex(e => new { e.Name, e.CategoryId }, "subcategories_name_categories_key").IsUnique();

            entity.Property(e => e.SubcategoryId)
                .ValueGeneratedNever()
                .HasColumnName("subcategory_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Subcategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_subcategories_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => new { e.Email, e.Phone }, "users_emai_phone_key").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Role)
                .HasColumnType("user_type")
                .HasColumnName("role");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
