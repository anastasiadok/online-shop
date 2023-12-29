using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IAddressService
{
    Task<AddressDto> GetById(Guid id);
    Task<IEnumerable<AddressDto>> GetAll();
    Task<bool> Add(AddressDto addressDto);
}