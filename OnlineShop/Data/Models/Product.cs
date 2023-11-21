namespace OnlineShop.Data.Models;

public class Product
{
    public Guid ProductId { get; set; }

    public Guid BrandId { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public float? AverageRating { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Media> Media { get; set; } = new List<Media>();

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
