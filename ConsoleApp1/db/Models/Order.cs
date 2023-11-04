using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public decimal TotalPrice { get; set; }

    public Guid AddressId { get; set; }

    public DateTime CreatedAt { get; set; }

    public TransactionStatus Status { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
