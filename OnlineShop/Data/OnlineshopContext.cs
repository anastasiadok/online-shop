using Microsoft.EntityFrameworkCore;
using Npgsql;
using OnlineShop.Data.Models;
using OnlineShop.Data.Maps;

namespace OnlineShop.Data;

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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("status_type", new[] { "in_review", "in_delivery", "completed", "cancelled" })
            .HasPostgresEnum("user_type", new[] { "admin", "user" });

        modelBuilder.ApplyConfiguration(new AddressMap());
        modelBuilder.ApplyConfiguration(new BrandMap());
        modelBuilder.ApplyConfiguration(new CartItemMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new ColorMap());
        modelBuilder.ApplyConfiguration(new MediaMap()); 
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new OrderItemMap());
        modelBuilder.ApplyConfiguration(new OrderTransactionMap()); 
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new ProductVariantMap());
        modelBuilder.ApplyConfiguration(new ReviewMap());
        modelBuilder.ApplyConfiguration(new SectionMap());
        modelBuilder.ApplyConfiguration(new SizeMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}
