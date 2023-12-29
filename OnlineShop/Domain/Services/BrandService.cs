using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class BrandService : BaseService, IBrandService
{
    public BrandService(OnlineshopContext context) : base(context) { }

    public async Task<bool> Add(BrandDto brand)
    {
        var newBrand = brand.Adapt<Brand>();
        newBrand.BrandId = Guid.NewGuid();

        await _context.Brands.AddAsync(newBrand);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<BrandDto> GetById(Guid id)
    {
        var brand = await _context.Brands.FindAsync(id);
        return brand.Adapt<BrandDto>();
    }

    public async Task<IEnumerable<BrandDto>> GetAll()
    {
        var brands = await _context.Brands.ToListAsync();
        return brands.Select(b => b.Adapt<BrandDto>());
    }

    public async Task<bool> RemoveById(Guid id)
    {
        var brand = await _context.Brands.FindAsync(id);

        if (brand is null)
            return false;

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeName(Guid id, string name)
    {
        var old = await _context.Brands.FindAsync(id);

        if (old is null)
            return false;

        old.Name = name;
        await _context.SaveChangesAsync();

        return true;
    }
}