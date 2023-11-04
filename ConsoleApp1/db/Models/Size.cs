using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class Size
{
    public Guid SizeId { get; set; }

    public string SizeName { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
