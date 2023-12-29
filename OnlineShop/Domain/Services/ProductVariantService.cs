using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class ProductVariantService : BaseService, IProductVariantService
{
    public ProductVariantService(OnlineshopContext context) : base(context) { }

    public async Task Add(ProductVariantCreationDto productVariantCreationDto)
    {
        _ = await _context.Colors.FindAsync(productVariantCreationDto.ColorId) ?? throw new BadRequestException("Color doesn't exist");
        _ = await _context.Sizes.FindAsync(productVariantCreationDto.SizeId) ?? throw new BadRequestException("Size doesn't exist");

        var productVariant = productVariantCreationDto.Adapt<ProductVariant>();
        productVariant.ProductVariantId = Guid.NewGuid();

        _context.ProductVariants.Add(productVariant);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductVariantDto>> GetAll()
    {
        return await _context.ProductVariants.ProjectToType<ProductVariantDto>().ToListAsync();
    }

    public async Task<ProductVariantDto> GetById(Guid id)
    {
        var productVariant = await _context.ProductVariants.FindAsync(id) ?? throw new NotFoundException("Product variant");
        return productVariant.Adapt<ProductVariantDto>();
    }

    public async Task<IEnumerable<ProductVariantDto>> GetVariantsForProduct(Guid productId)
    {
        var product = await _context.Products.Where(p => p.ProductId == productId).Include(p => p.ProductVariants).FirstOrDefaultAsync();
        if (product is null)
            return null;

        return product.ProductVariants.Select(p => p.Adapt<ProductVariantDto>());
    }
}