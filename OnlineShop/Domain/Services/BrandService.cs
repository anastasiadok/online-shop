using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
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
    public async Task<bool> Add(BrandDto brand)
    {
        Brand newBrand = new()
        {
            BrandId = Guid.NewGuid(),
            Name = brand.Name,
            Products = brand.Products
        };

        await _context.Brands.AddAsync(newBrand);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Brand> Get(Guid id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public IEnumerable<Brand> GetAll()
    {
        return _context.Brands.ToList();
    }

    public async Task<bool> Remove(Guid id)
    {
        var brand = await _context.Brands.FindAsync(id);

        if (brand is null)
            return false;

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(Guid id, BrandDto brand)
    {
        var old = await _context.Brands.FindAsync(id);

        if (old is null)
            return false;

        old.Name = brand.Name;
        old.Products = brand.Products;
        _context.Brands.Update(old);
        await _context.SaveChangesAsync();

        return true;
    }
}
