using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

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
    public async Task<ActionResult<IEnumerable<ProductVariantDto>>> Get([FromRoute] Guid productid)
    {
        var productVariants = await _productVariantService.GetVariantsForProduct(productid);

        if (productVariants is null)
            return NotFound();

        return Ok(productVariants);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductVariantCreationDto productVariantCreationDto)
    {
        bool result = await _productVariantService.Add(productVariantCreationDto);

        if (!result)
            return BadRequest();

        return Ok();
    }
}
