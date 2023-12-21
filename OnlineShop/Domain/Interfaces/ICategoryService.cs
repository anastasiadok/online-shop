using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface ICategoryService
{
    Task<bool> Add(CategoryDto category);
    Task<CategoryDto> GetById(Guid id);
    Task<bool> ChangeName(Guid id, string name);
    Task<bool> ChangeParentCategory(Guid id, Guid parentCategoryId);
    Task<bool> ChangeSection(Guid id, Guid sectionId);


}