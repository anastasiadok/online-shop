using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public class BrandDto
{
    public string Name {  get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}
