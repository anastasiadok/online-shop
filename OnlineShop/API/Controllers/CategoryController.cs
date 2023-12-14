using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services;

namespace OnlineShop.API.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController:Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> Get([FromRoute] Guid id)
    {
        var category = await _categoryService.GetById(id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoryDto categoryDto)
    {
        bool result = await _categoryService.Add(categoryDto);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpPatch("{id}/name/{name}")]
    public async Task<IActionResult> UpdateName([FromRoute] Guid id, [FromRoute] string name)
    {
        bool result = await _categoryService.ChangeName(id, name);

        if (!result)
            return NotFound();

        return Ok();
    }

    [HttpPatch("{id}/parentcategory/{parentcategoryid}")]
    public async Task<IActionResult> UpdateParentCategory([FromRoute] Guid id, [FromRoute] Guid pcategoryid)
    {
        bool result = await _categoryService.ChangeParentCategory(id, pcategoryid);

        if (!result)
            return NotFound();

        return Ok();
    }

    [HttpPatch("{id}/section/{sectionid}")]
    public async Task<IActionResult> UpdateSection([FromRoute] Guid id, [FromRoute] Guid sectionid)
    {
        bool result = await _categoryService.ChangeSection(id, sectionid);

        if (!result)
            return NotFound();

        return Ok();
    }
}
