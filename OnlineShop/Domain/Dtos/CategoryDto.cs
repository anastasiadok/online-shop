using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public record CategoryDto(Guid CategoryId, Guid SectionId, Guid? ParentCategoryId, string Name, ICollection<CategoryDto> Categories);
