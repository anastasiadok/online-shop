namespace OnlineShop.Data.Models;

public class ProductVariant
{
    public Guid ProductVariantId { get; set; }

    public Guid ColorId { get; set; }

    public Guid SizeId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string Sku { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Color Color { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
