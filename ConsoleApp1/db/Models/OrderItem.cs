using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class OrderItem
{
    public Guid ProductVariantId { get; set; }

    public Guid OrderId { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
