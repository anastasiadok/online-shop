namespace OnlineShop.Data.Models;

public class Color
{
    public Guid ColorId { get; set; }

    public string ColorName { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
