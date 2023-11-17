namespace ConsoleApp1.db.Models;

public class Product
{
    public Guid ProductId { get; set; }

    public Guid BrandId { get; set; }

    public Guid SubcategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public float? AverageRating { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Subcategory Subcategory { get; set; } = null!;

    public virtual ICollection<Media> Media { get; set; } = new List<Media>();

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
