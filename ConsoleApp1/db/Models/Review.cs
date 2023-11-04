using System;
using System.Collections.Generic;

namespace ConsoleApp1.db.Models;

public partial class Review
{
    public Guid ReviewId { get; set; }

    public Guid ProductId { get; set; }

    public Guid UserId { get; set; }

    public int Rating { get; set; }

    public string? CommentText { get; set; }

    public string? Title { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
