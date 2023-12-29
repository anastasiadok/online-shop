using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public record ProductCreationDto(Guid BrandId, Guid CategoryId, string Name, decimal Price);
