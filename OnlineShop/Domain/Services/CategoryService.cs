using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class CategoryService : BaseService, ICategoryService
{
    public CategoryService(OnlineshopContext context) : base(context) { }

    public async Task<bool> Add(CategoryDto categoryDto)
    {
        var category = categoryDto.Adapt<Category>();
        category.CategoryId = Guid.NewGuid();

        var section = await _context.Sections.FindAsync(category.SectionId);

        if (section is null)
            return false;

        if (category.ParentCategoryId is not null)
        {
            var pCategory = await _context.Categories.FindAsync(category.ParentCategoryId);

            if (pCategory is null)
                return false;
            if (pCategory.SectionId != section.SectionId)
                return false;
        }

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeName(Guid id, string name)
    {
        var old = await _context.Categories.FindAsync(id);

        if (old is null) return false;

        old.Name = name;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeParentCategory(Guid id, Guid parentCategoryId)
    {
        var old = await _context.Categories.Include(c => c.Categories).Where(c => c.CategoryId == id).SingleOrDefaultAsync();
        if (old is null)
            return false;

        var pcategory = await _context.Categories.FindAsync(parentCategoryId);
        if (pcategory is null)
            return false;

        if (!IsChangingValid(old, pcategory))
            return false;

        old.ParentCategoryId = parentCategoryId;
        old.SectionId = pcategory.SectionId;
        await _context.SaveChangesAsync();

        return true;
    }

    private static bool IsChangingValid(Category node, Category parent)
    {
        Stack<Category> stack = new();
        stack.Push(node);

        while (stack.Count > 0)
        {
            Category current = stack.Pop();
            if (current.CategoryId == parent.CategoryId)
                return false;

            foreach (var child in current.Categories)
                stack.Push(child);
        }

        return true;
    }

    public async Task<bool> ChangeSection(Guid id, Guid sectionId)
    {
        var old = await _context.Categories.FindAsync(id);
        if (old is null)
            return false;

        var section = await _context.Sections.FindAsync(sectionId);
        if (section is null)
            return false;

        if (old.SectionId != sectionId)
        {
            old.SectionId = sectionId;
            old.ParentCategoryId = null;
            await _context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<CategoryDto> GetById(Guid id)
    {
        var category = await _context.Categories.Include(c => c.Categories).Where(c => c.CategoryId == id).SingleOrDefaultAsync();
        return category?.Adapt<CategoryDto>();
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        return await _context.Categories.Select(c => c.Adapt<CategoryDto>()).ToListAsync();
    }
}