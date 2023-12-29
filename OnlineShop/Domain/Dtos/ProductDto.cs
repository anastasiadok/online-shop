using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public record ProductDto(Guid ProductId, Guid BrandId, Guid CategoryId, string Name, decimal Price, float? AverageRating,
    ICollection<MediaDto> Media, ICollection<ProductVariantDto> ProductVariants);
