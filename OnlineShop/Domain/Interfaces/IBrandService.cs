using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IBrandService
{
    Task Add(BrandDto brand);
    Task<IEnumerable<BrandDto>> GetAll();
    Task<BrandDto> GetById(Guid id);
    Task RemoveById(Guid id);
    Task ChangeName(Guid id, string name);
}