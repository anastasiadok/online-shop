using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[AllowAnonymous]
[Route("api/sizes")]
[ApiController]
public class SizeController : Controller
{
    private readonly ISizeService _sizeService;
    public SizeController(ISizeService sizeService)
    {
        _sizeService = sizeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SizeDto>>> GetAll()
    {
        var sizes = await _sizeService.GetAll();
        return Ok(sizes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SizeDto>> GetById([FromRoute] Guid id)
    {
        var size = await _sizeService.GetById(id);
        return Ok(size);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SizeDto sizeDto)
    {
        await _sizeService.Add(sizeDto);
        return Ok();
    }
}