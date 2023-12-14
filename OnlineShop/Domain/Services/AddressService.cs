using Mapster;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class AddressService : BaseService, IAddressService
{
    public AddressService(OnlineshopContext context) : base(context) { }

    public async Task<AddressDto> GetById(Guid id)
    {
        var address = await _context.Addresses.FindAsync(id);
        return address?.Adapt<AddressDto>();
    }

    public async Task<bool> Add(AddressDto addressDto)
    {
        var address = addressDto.Adapt<Address>();
        address.AddressId = Guid.NewGuid();

        var user = await _context.Users.FindAsync(address.UserId);

        if (user is null)
            return false;

        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();

        return true;
    }
}