using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface ISizeService
{
    Task<IEnumerable<SizeDto>> GetAll();
    Task<SizeDto> GetById(Guid id);
    Task Add(SizeDto sizeDto);
}