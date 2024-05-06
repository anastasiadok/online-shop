using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[AllowAnonymous]
[Route("api/brands")]
[ApiController]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandDto>> GetById([FromRoute] Guid id)
    {
        var brand = await _brandService.GetById(id);
        return Ok(brand);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll()
    {
        var brands = await _brandService.GetAll();
        return Ok(brands);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] BrandDto brand)
    {
        await _brandService.Add(brand);
        return Ok();
    }

    [HttpPatch("{id}/name/{name}")]
    public async Task<IActionResult> UpdateName([FromRoute] Guid id, [FromRoute] string name)
    {
        await _brandService.ChangeName(id, name);
        return Ok();
    }
}