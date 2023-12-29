using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[Route("api/cartitems")]
[ApiController]
public class CartItemController : Controller
{
    private readonly ICartItemService _cartItemService;
    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [AllowAnonymous]
    [HttpGet("users/{userid}")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetByUserId([FromRoute] Guid userid)
    {
        var cartItem = await _cartItemService.GetUserCartItems(userid);
        return Ok(cartItem);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CartItemDto cartItem)
    {
        await _cartItemService.Add(cartItem);
        return Ok();
    }

    [Authorize]
    [HttpPatch("{userid}/{productvariantid}/quantity/{quantity}")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromRoute] Guid productvariantid, [FromRoute] int quantity)
    {
        await _cartItemService.ChangeQuantity(userId, productvariantid, quantity);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{userid}/{productvariantid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid userid, [FromRoute] Guid productvariantid)
    {
        await _cartItemService.RemoveByKey(userid, productvariantid);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<CartItemDto>> GetAll()
    {
        var items = await _cartItemService.GetAll();
        return Ok(items);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<CartItemDto>> GetById([FromQuery] Guid id)
    {
        var item = await _cartItemService.GetById(id);
        return Ok(item);
    }
}
