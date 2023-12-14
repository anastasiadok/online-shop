using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IBrandService
{
    Task<bool> Add(BrandDto brand);
    Task<IEnumerable<BrandDto>> GetAll();
    Task<BrandDto> GetById(Guid id);
    Task<bool> RemoveById(Guid id);
    Task<bool> ChangeName(Guid id, string name);
}