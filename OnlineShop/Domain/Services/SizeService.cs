using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class SizeService : BaseService, ISizeService
{
    public SizeService(OnlineshopContext context) : base(context) { }

    public async Task<IEnumerable<SizeDto>> GetAll()
    {
        return await _context.Sizes.Select(c => c.Adapt<SizeDto>()).ToListAsync();
    }
    public async Task<SizeDto> GetById(Guid id)
    {
        var color = await _context.Sizes.FindAsync(id);
        return color?.Adapt<SizeDto>();
    }

    public async Task<bool> Add(SizeDto sizeDto)
    {
        var size = sizeDto.Adapt<Size>();
        size.SizeId = Guid.NewGuid();

        await _context.Sizes.AddAsync(size);
        await _context.SaveChangesAsync();

        return true;
    }
}