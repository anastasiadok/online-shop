using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IColorService
{
    Task<IEnumerable<ColorDto>> GetAll();
    Task<ColorDto> GetById(Guid id);
    Task<bool> Add(ColorDto colorDto);
}