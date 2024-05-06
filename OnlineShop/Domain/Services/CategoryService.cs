using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services.Extensions;

namespace OnlineShop.Domain.Services;

public class CategoryService : BaseService, ICategoryService
{
    public CategoryService(OnlineshopContext context) : base(context) { }

    public async Task Add(CategoryDto categoryDto)
    {
        var category = categoryDto.Adapt<Category>();
        category.CategoryId = Guid.NewGuid();

        var section = await _context.Sections.FindAsync(category.SectionId) ?? throw new BadRequestException("Section doesn't exist");

        if (category.ParentCategoryId is not null)
        {
            var pCategory = await _context.Categories.FindAsync(category.ParentCategoryId);

            if (pCategory is null)
                throw new BadRequestException("Parent category doesn't exist");
            if (pCategory.SectionId != section.SectionId)
                throw new BadRequestException("Section is incorrect");
        }

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeName(Guid id, string name)
    {
        var old = await _context.Categories.FindAsync(id) ?? throw new NotFoundException("Category");

        old.Name = name;
        await _context.SaveChangesAsync();
    }

    public static List<T> GetParents<T>(TreeExtensions.ITree<T> node, List<T> parentNodes = null) where T : class
    {
        while (true)
        {
            parentNodes ??= new List<T>();
            if (node?.Parent?.Data == null) return parentNodes;
            parentNodes.Add(node.Parent.Data);
            node = node.Parent;
        }
    }

    public async Task<bool> ChangeParentCategory(Guid id, Guid parentCategoryId)
    {
        List<Category> all = _context.Categories.Include(x => x.ParentCategory).ToList();
        TreeExtensions.ITree<Category> virtualRootNode = all.ToTree((parent, child) => child.ParentCategoryId == parent.CategoryId);
        List<TreeExtensions.ITree<Category>> rootLevelCategoriesWithSubTree = virtualRootNode.Children.ToList();
        List<TreeExtensions.ITree<Category>> flattenedListOfCategoryNodes = virtualRootNode.Children.Flatten(node => node.Children).ToList();

        TreeExtensions.ITree<Category> categoryNode = flattenedListOfCategoryNodes.First(node => node.Data.CategoryId == id);
        TreeExtensions.ITree<Category> parentNode = flattenedListOfCategoryNodes.First(node => node.Data.CategoryId == parentCategoryId);

        Category old = categoryNode.Data;
        Category parent = parentNode.Data;

        if (old is null)
            return false;
        if (parent is null)
            return false;

        if (!IsChangingValid(old, parent))
            return false;

        old.ParentCategoryId = parentCategoryId;
        old.SectionId = parent.SectionId;
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

    public async Task ChangeSection(Guid id, Guid sectionId)
    {
        var old = await _context.Categories.FindAsync(id) ?? throw new NotFoundException("Category");

        _ = await _context.Sections.FindAsync(sectionId) ?? throw new NotFoundException("Section");

        if (old.SectionId != sectionId)
        {
            old.SectionId = sectionId;
            old.ParentCategoryId = null;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Category>> GetSectionCategories(Guid sectionId)
    {
        Section section = await _context.Sections.Include(s => s.Categories).Where(s=>s.SectionId == sectionId).FirstOrDefaultAsync();
        if (section is null)
            throw new NotFoundException("Section");

        var categories = section.Categories.Where(c => c.ParentCategoryId is null).ToList();
        TreeExtensions.ITree<Category> virtualRootNode = categories.ToTree((parent, child) => child.ParentCategoryId == parent.CategoryId);
        List<TreeExtensions.ITree<Category>> rootLevelCategoriesWithSubTree = virtualRootNode.Children.ToList();
        List<TreeExtensions.ITree<Category>> flattenedListOfCategoryNodes = virtualRootNode.Children.Flatten(node => node.Children).ToList();
        List<Category> result = new();
        foreach (var tree in flattenedListOfCategoryNodes)
        {
            result.AddRange(GetParents(tree));
        }

        return result;
    }

    public async Task<CategoryDto> GetById(Guid id)
    {
        var category = await _context.Categories.Include(c => c.Categories).Where(c => c.CategoryId == id).SingleOrDefaultAsync();
        if (category is null)
            throw new NotFoundException("Category");

        return category.Adapt<CategoryDto>();
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        return await _context.Categories.ProjectToType<CategoryDto>().ToListAsync();
    }
}