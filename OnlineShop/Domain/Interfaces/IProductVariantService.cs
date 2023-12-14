using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IProductVariantService
{
    Task<IEnumerable<ProductVariantDto>> GetVariantsForProduct(Guid productId);
    Task<bool> Add(ProductVariantCreationDto productVariantCreationDto);
}