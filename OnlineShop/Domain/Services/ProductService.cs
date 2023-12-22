using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;
using Sieve.Models;
using Sieve.Services;

namespace OnlineShop.Domain.Services;

public class ProductService : BaseService, IProductService
{
    private readonly SieveProcessor _sieveProcessor;
    public ProductService(OnlineshopContext context, SieveProcessor sieveProcessor) : base(context)
    {
        _sieveProcessor = sieveProcessor;
    }

    public async Task<bool> Add(ProductCreationDto productCreationDto)
    {
        var brand = await _context.Brands.FindAsync(productCreationDto.BrandId);
        if (brand is null)
            return false;

        var category = await _context.Categories.FindAsync(productCreationDto.CategoryId);
        if (category is null)
            return false;

        var product = productCreationDto.Adapt<Product>();
        product.ProductId = Guid.NewGuid();

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeCategory(Guid id, Guid categoryId)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return false;

        var category = await _context.Categories.FindAsync(categoryId);
        if (category is null)
            return false;

        product.CategoryId = categoryId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ProductDto> GetById(Guid id)
    {
        var product = await _context.Products.Where(p => p.ProductId == id).Include(p => p.ProductVariants).Include(p => p.Media).FirstAsync();
        return product?.Adapt<ProductDto>();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByFilter([FromBody] SieveModel sieveModel)
    {
        var result = _context.Products.Include(p => p.ProductVariants).AsNoTracking();
        var products = await _sieveProcessor.Apply(sieveModel, result).ToListAsync();
        return products.Select(p => p.Adapt<ProductDto>());
    }

    public async Task<IEnumerable<ProductDto>> GetCategoryProducts(Guid categoryId)
    {
        var category = await _context.Categories.Include(c => c.Categories).Include(c => c.Products).Where(c => c.CategoryId == categoryId).FirstOrDefaultAsync();
        if (category is null)
            return null;

        List<ProductDto> products = new();

        Stack<Category> stack = new();
        stack.Push(category);

        while (stack.Count > 0)
        {
            Category current = stack.Pop();
            products.AddRange(current.Products.Select(p => p.Adapt<ProductDto>()));

            foreach (var child in current.Categories)
                stack.Push(child);
        }

        return products;
    }

    public async Task<IEnumerable<ProductDto>> GetAll()
    {
        return await _context.Products.Select(p => p.Adapt<ProductDto>()).ToListAsync();
    }
}