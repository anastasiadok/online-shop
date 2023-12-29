using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[AllowAnonymous]
[Route("api/categories")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById([FromRoute] Guid id)
    {
        var category = await _categoryService.GetById(id);
        return Ok(category);
    }

    [HttpGet]
    public async Task<ActionResult<CategoryDto>> GetAll()
    {
        var categories = await _categoryService.GetAll();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoryDto categoryDto)
    {
        await _categoryService.Add(categoryDto);
        return Ok();
    }

    [HttpPatch("{id}/name/{name}")]
    public async Task<IActionResult> UpdateName([FromRoute] Guid id, [FromRoute] string name)
    {
        await _categoryService.ChangeName(id, name);
        return Ok();
    }

    [HttpPatch("{id}/parentcategory/{parentcategoryid}")]
    public async Task<IActionResult> UpdateParentCategory([FromRoute] Guid id, [FromRoute] Guid parentcategoryid)
    {
        await _categoryService.ChangeParentCategory(id, parentcategoryid);
        return Ok();
    }

    [HttpGet("sectionscategories/{sectionid}")]
    public async Task<ActionResult<IEnumerable<Category>>> GetSectionCategories([FromRoute] Guid sectionid)
    {
        var result = await _categoryService.GetSectionCategories(sectionid);
        return Ok(result);
    }

    [HttpPatch("{id}/section/{sectionid}")]
    public async Task<IActionResult> UpdateSection([FromRoute] Guid id, [FromRoute] Guid sectionid)
    {
        await _categoryService.ChangeSection(id, sectionid);
        return Ok();
    }
}
