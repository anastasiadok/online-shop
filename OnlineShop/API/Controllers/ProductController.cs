using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;
using Sieve.Models;

namespace OnlineShop.API.Controllers;

[AllowAnonymous]
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
    public async Task<ActionResult<ProductDto>> GetById([FromRoute] Guid id)
    {
        var product = await _productService.GetById(id);
        return Ok(product);
    }

    [HttpGet("category/{categoryid}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetCategoryProducts([FromRoute] Guid categoryid)
    {
        var products = await _productService.GetCategoryProducts(categoryid);
        return Ok(products);
    }

    [HttpGet("filtered")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByFilter([FromQuery] SieveModel sieveModel)
    {
        var products = await _productService.GetProductsByFilter(sieveModel);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductCreationDto product)
    {
        await _productService.Add(product);
        return Ok();
    }

    [HttpPatch("{id}/category/{categoryid}")]
    public async Task<IActionResult> ChangeCategory([FromRoute] Guid id, [FromRoute] Guid categoryid)
    {
        await _productService.ChangeCategory(id, categoryid);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<ProductDto>> GetAll()
    {
        var products = await _productService.GetAll();
        return Ok(products);
    }
}
