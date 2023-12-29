using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public record CartItemDto(Guid ProductVariantId,Guid UserId, int Quantity, ProductVariantDto ProductVariant);
