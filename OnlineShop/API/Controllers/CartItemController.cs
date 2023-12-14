using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[Route("api/cartitems")]
[ApiController]
public class CartItemController:Controller
{
    private readonly ICartItemService _cartItemService;
    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService= cartItemService;
    }

    [HttpGet("users/{userid}")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> Get([FromRoute] Guid userid)
    {
        var cartItem = await _cartItemService.GetUserCartItems(userid);
        return Ok(cartItem);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CartItemDto cartItem)
    {
        bool result = await _cartItemService.Add(cartItem);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpPatch("{userid}/{productvariantid}/quantity/{quantity}")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromRoute] Guid productvariantid, [FromRoute] int quantity)
    {
        bool result = await _cartItemService.ChangeQuantity(userId, productvariantid, quantity);

        if (!result)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{userid}/{productvariantid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid userid, [FromRoute] Guid productvariantid)
    {
        bool result = await _cartItemService.RemoveByKey(userid, productvariantid);

        if (!result)
            return NotFound();

        return Ok();
    }
}
