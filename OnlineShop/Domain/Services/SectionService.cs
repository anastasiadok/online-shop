using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class SectionService : BaseService, ISectionService
{
    public SectionService(OnlineshopContext context) : base(context) { }

    public async Task<bool> Add(SectionDto section)
    {
        var newSection = section.Adapt<Section>();
        newSection.SectionId = Guid.NewGuid();
        _context.Sections.Add(newSection);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeName(Guid id, string name)
    {
        var section = await _context.Sections.FindAsync(id);
        if (section is null)
            return false;

        section.Name = name;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<SectionDto>> GetAll()
    {
        var sections = await _context.Sections.Include(s => s.Categories).ToListAsync();
        return sections.Select(m => m.Adapt<SectionDto>());
    }

    public async Task<SectionDto> GetById(Guid id)
    {
        var section = await _context.Sections.FindAsync(id);
        return section.Adapt<SectionDto>();
    }
}