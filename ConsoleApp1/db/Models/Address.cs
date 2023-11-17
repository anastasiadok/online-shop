namespace ConsoleApp1.db.Models;

public class Address
{
    public Guid AddressId { get; set; }

    public Guid UserId { get; set; }

    public string Address1 { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
