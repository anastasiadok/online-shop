using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;
using Sieve.Models;

namespace OnlineShop.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get([FromRoute] Guid id)
    {
        var product =await _productService.GetById(id);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet("category/{categoryid}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetCategoryProducts([FromRoute] Guid categoryid)
    {
        var products = await _productService.GetCategoryProducts(categoryid);

        if (products is null)
            return NotFound();

        return Ok(products);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByFilter([FromBody] SieveModel sieveModel)
    {
        var products = await _productService.GetProductsByFilter(sieveModel);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductCreationDto product)
    {
        bool result = await _productService.Add(product);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpPatch("{id}/category/{categoryid}")]
    public async Task<IActionResult> ChangeCategory([FromRoute] Guid id, [FromRoute] Guid categoryid)
    { 
        bool result = await _productService.ChangeCategory(id, categoryid);

        if (!result)
            return NotFound();

        return Ok();
    }
}
