using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class BrandService : BaseService, IBrandService
{
    public BrandService(OnlineshopContext context) : base(context) { }

    public async Task Add(BrandDto brand)
    {
        var newBrand = brand.Adapt<Brand>();
        newBrand.BrandId = Guid.NewGuid();

        await _context.Brands.AddAsync(newBrand);
        await _context.SaveChangesAsync();
    }

    public async Task<BrandDto> GetById(Guid id)
    {
        var brand = await _context.Brands.FindAsync(id) ?? throw new NotFoundException("Brand");
        return brand.Adapt<BrandDto>();
    }

    public async Task<IEnumerable<BrandDto>> GetAll()
    {
        return await _context.Brands.ProjectToType<BrandDto>().ToListAsync();
    }

    public async Task RemoveById(Guid id)
    {
        var brand = await _context.Brands.FindAsync(id) ?? throw new NotFoundException("Brand");

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeName(Guid id, string name)
    {
        var old = await _context.Brands.FindAsync(id) ?? throw new NotFoundException("Brand");

        old.Name = name;
        await _context.SaveChangesAsync();
    }
}