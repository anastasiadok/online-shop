using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class ProductVariantService : BaseService, IProductVariantService
{
    public ProductVariantService(OnlineshopContext context) : base(context) { }

    public async Task<bool> Add(ProductVariantCreationDto productVariantCreationDto)
    {
        var color = await _context.Colors.FindAsync(productVariantCreationDto.ColorId);
        if (color is null)
            return false;

        var size = await _context.Sizes.FindAsync(productVariantCreationDto.SizeId);
        if (size is null)
            return false;

        var productVariant = productVariantCreationDto.Adapt<ProductVariant>();
        productVariant.ProductVariantId = Guid.NewGuid();
        return true;
    }

    public async Task<IEnumerable<ProductVariantDto>> GetVariantsForProduct(Guid productId)
    {
        var product = await _context.Products.Where(p => p.ProductId == productId).Include(p => p.ProductVariants).FirstOrDefaultAsync();
        if (product is null)
            return null;

        return product.ProductVariants.Select(p => p.Adapt<ProductVariantDto>());
    }
}