using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class SectionService : BaseService, ISectionService
{
    public SectionService(OnlineshopContext context) : base(context) { }

    public async Task Add(SectionDto section)
    {
        var newSection = section.Adapt<Section>();
        newSection.SectionId = Guid.NewGuid();

        _context.Sections.Add(newSection);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeName(Guid id, string name)
    {
        var section = await _context.Sections.FindAsync(id) ?? throw new NotFoundException("Section");

        section.Name = name;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SectionDto>> GetAll()
    {
        return await _context.Sections.Include(s => s.Categories).ProjectToType<SectionDto>().ToListAsync();
    }

    public async Task<SectionDto> GetById(Guid id)
    {
        var section = await _context.Sections.FindAsync(id) ?? throw new NotFoundException("Section");
        return section.Adapt<SectionDto>();
    }
}