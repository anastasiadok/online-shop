namespace OnlineShop.Domain.Dtos;

public record MediaCreationDto(Guid ProductId, IFormFile File);