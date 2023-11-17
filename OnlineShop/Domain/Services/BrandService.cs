using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace OnlineShop.Domain.Services;

public class BrandService : IBrandService
{
    private readonly OnlineshopContext _context;

    public BrandService(OnlineshopContext context)
    {
        _context = context;
    }
    public async Task Add(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
        await _context.SaveChangesAsync();
    }

    public async Task<Brand> Get(Guid id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public IEnumerable<Brand> GetAll()
    {
        return _context.Brands.ToList();
    }

    public async Task Remove(Guid id)
    {
        var b = await _context.Brands.FindAsync(id);
        _context.Brands.Remove(b);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Brand brand)
    {
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
    }
}
