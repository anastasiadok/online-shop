using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[Route("api/sections")]
[ApiController]
public class SectionController : Controller
{
    private readonly ISectionService _sectionService;
    public SectionController(ISectionService sectiontService)
    {
        _sectionService = sectiontService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SectionDto>> Get([FromRoute] Guid id)
    {
        var product = await _sectionService.GetById(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SectionDto>>> GetAll()
    {
        var sections = await _sectionService.GetAll();
        return Ok(sections);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SectionDto section)
    {
        bool result = await _sectionService.Add(section);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpPatch("{id}/name/{name}")]
    public async Task<IActionResult> ChangeName([FromRoute] Guid id, [FromRoute] string name)
    {
        bool result = await _sectionService.ChangeName(id, name);
        if (!result)
            return NotFound();

        return Ok();
    }
}
