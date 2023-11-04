using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class OrderTransaction
{
    public Guid OrderId { get; set; }

    public TransactionStatus Status { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
