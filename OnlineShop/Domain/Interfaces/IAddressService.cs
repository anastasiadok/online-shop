using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IAddressService
{
    Task<AddressDto> GetById(Guid id);
    Task<bool> Add(AddressDto addressDto);
}