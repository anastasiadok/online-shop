namespace OnlineShop.Data.Models;

public class OrderTransaction
{
    public Guid OrderId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
