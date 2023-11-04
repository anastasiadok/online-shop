using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class Media
{
    public Guid MediaId { get; set; }

    public Guid ProductId { get; set; }

    public byte[] Bytes { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
