namespace OnlineShop.Domain.Dtos;

public record BrandDto(Guid BrandId, string Name, ICollection<ProductDto> Products);