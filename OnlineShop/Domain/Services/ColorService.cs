using Mapster;
using OnlineShop.Data.Models;
using OnlineShop.Data;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Exceptions;

namespace OnlineShop.Domain.Services;

public class ColorService : BaseService, IColorService
{
    public ColorService(OnlineshopContext context) : base(context) { }

    public async Task<IEnumerable<ColorDto>> GetAll()
    {
        return await _context.Colors.ProjectToType<ColorDto>().ToListAsync();
    }
    public async Task<ColorDto> GetById(Guid id)
    {
        var color = await _context.Colors.FindAsync(id) ?? throw new NotFoundException("Color");
        return color.Adapt<ColorDto>();
    }

    public async Task Add(ColorDto colorDto)
    {
        var color = colorDto.Adapt<Color>();
        color.ColorId = Guid.NewGuid();

        await _context.Colors.AddAsync(color);
        await _context.SaveChangesAsync();
    }
}