using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[Route("api/brands")]
[ApiController]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll()
    {
        var brands = await _brandService.GetAll();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandDto>> Get([FromRoute] Guid id)
    {
        var brand = await _brandService.GetById(id);

        if (brand is null)
            return NotFound();

        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] BrandDto brand)
    {
        bool result = await _brandService.Add(brand);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpPatch("{id}/name/{name}")]
    public async Task<IActionResult> UpdateName([FromRoute] Guid id, [FromRoute] string name)
    {
        bool result = await _brandService.ChangeName(id,name);

        if (!result)
            return NotFound();

        return Ok();
    }
}