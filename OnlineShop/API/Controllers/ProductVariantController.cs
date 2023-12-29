using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[AllowAnonymous]
[Route("api/productvariants")]
[ApiController]
public class ProductVariantController : Controller
{
    private readonly IProductVariantService _productVariantService;
    public ProductVariantController(IProductVariantService productVariantService)
    {
        _productVariantService = productVariantService;
    }

    [HttpGet("products/{productid}")]
    public async Task<ActionResult<IEnumerable<ProductVariantDto>>> GetByProduct([FromRoute] Guid productid)
    {
        var productVariants = await _productVariantService.GetVariantsForProduct(productid);
        return Ok(productVariants);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductVariantCreationDto productVariantCreationDto)
    {
        await _productVariantService.Add(productVariantCreationDto);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<ProductVariantDto>> GetAll()
    {
        var productVariants = await _productVariantService.GetAll();
        return Ok(productVariants);
    }
}
