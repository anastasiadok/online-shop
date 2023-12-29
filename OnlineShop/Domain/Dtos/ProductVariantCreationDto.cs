namespace OnlineShop.Domain.Dtos;

public record ProductVariantCreationDto(Guid ColorId, Guid SizeId, Guid ProductId, int Quantity, string Sku);