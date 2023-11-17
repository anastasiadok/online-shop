using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Interfaces;

public interface IBrandService
{
    Task Add(Brand brand);
    IEnumerable<Brand> GetAll();
    Task<Brand> Get(Guid id);
    Task Remove(Guid id);
    Task Update(Brand brand);
}
