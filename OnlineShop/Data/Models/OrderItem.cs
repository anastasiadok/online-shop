namespace OnlineShop.Data.Models;

public class OrderItem
{
    public Guid ProductVariantId { get; set; }

    public Guid OrderId { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
