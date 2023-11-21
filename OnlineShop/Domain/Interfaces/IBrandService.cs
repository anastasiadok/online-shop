using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IBrandService
{
    Task<bool> Add(BrandDto brand);
    IEnumerable<Brand> GetAll();
    Task<Brand> Get(Guid id);
    Task <bool> Remove(Guid id);
    Task<bool> Update(Guid id, BrandDto brand);
}
