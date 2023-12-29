using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface ICategoryService
{
    Task Add(CategoryDto category);
    Task<CategoryDto> GetById(Guid id);
    Task ChangeName(Guid id, string name);
    Task<bool> ChangeParentCategory(Guid id, Guid parentCategoryId);
    Task<IEnumerable<Category>> GetSectionCategories(Guid sectionId);
    Task ChangeSection(Guid id, Guid sectionId);
    Task<IEnumerable<CategoryDto>> GetAll();
}