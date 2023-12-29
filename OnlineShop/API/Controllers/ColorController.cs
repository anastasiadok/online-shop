using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[AllowAnonymous]
[Route("api/colors")]
[ApiController]
public class ColorController : Controller
{
    private readonly IColorService _colorService;
    public ColorController(IColorService colorService)
    {
        _colorService = colorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ColorDto>>> GetAll()
    {
        var colors = await _colorService.GetAll();
        return Ok(colors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ColorDto>> GetById([FromRoute] Guid id)
    {
        var color = await _colorService.GetById(id);
        return Ok(color);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ColorDto colorDto)
    {
        await _colorService.Add(colorDto);
        return Ok();
    }
}
