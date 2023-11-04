﻿using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class Color
{
    public Guid ColorId { get; set; }

    public string ColorName { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
