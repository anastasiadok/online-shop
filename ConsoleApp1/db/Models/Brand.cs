using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class Brand
{
    public Guid BrandId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
