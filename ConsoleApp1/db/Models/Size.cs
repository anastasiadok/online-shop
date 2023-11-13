namespace ConsoleApp1.db.Models;

public class Size
{
    public Guid SizeId { get; set; }

    public string SizeName { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
