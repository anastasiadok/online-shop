﻿using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class CartItem
{
    public Guid ProductVariantId { get; set; }

    public Guid UserId { get; set; }

    public int Quantity { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
