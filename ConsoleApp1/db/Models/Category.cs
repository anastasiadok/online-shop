﻿using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public Guid SectionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

    public virtual Section Section { get; set; } = null!;
}