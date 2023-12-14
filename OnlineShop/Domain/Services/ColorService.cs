using Mapster;
using OnlineShop.Data.Models;
using OnlineShop.Data;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Domain.Services;

public class ColorService : BaseService, IColorService
{
    public ColorService(OnlineshopContext context) : base(context) { }

    public async Task<IEnumerable<ColorDto>> GetAll()
    {
        return await _context.Colors.Select(c => c.Adapt<ColorDto>()).ToListAsync();
    }
    public async Task<ColorDto> GetById(Guid id)
    {
        var color = await _context.Colors.FindAsync(id);
        return color?.Adapt<ColorDto>();
    }

    public async Task<bool> Add(ColorDto colorDto)
    {
        var color = colorDto.Adapt<Color>();
        color.ColorId = Guid.NewGuid();

        await _context.Colors.AddAsync(color);
        await _context.SaveChangesAsync();

        return true;
    }
}