using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class AddressService : BaseService, IAddressService
{
    public AddressService(OnlineshopContext context) : base(context) { }

    public async Task<AddressDto> GetById(Guid id)
    {
        var address = await _context.Addresses.FindAsync(id) ?? throw new NotFoundException("Address");
        return address.Adapt<AddressDto>();
    }

    public async Task<IEnumerable<AddressDto>> GetAll()
    {
        return await _context.Addresses.ProjectToType<AddressDto>().ToListAsync();
    }

    public async Task Add(AddressDto addressDto)
    {
        _ = await _context.Users.FindAsync(addressDto.UserId) ?? throw new BadRequestException("User doesn't exist");

        var address = addressDto.Adapt<Address>();
        address.AddressId = Guid.NewGuid();
        
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
    }
}