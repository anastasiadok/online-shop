namespace OnlineShop.Domain.Dtos;

public record SectionDto(Guid SectionId, string Name, ICollection<CategoryDto> Categories);
