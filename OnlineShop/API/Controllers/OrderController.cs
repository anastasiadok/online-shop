using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> Get([FromRoute] Guid id)
    {
        var brand = await _orderService.GetById(id);

        if (brand is null)
            return NotFound();

        return Ok(brand);
    }

    [HttpGet("users/{userid}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders(Guid userid)
    {
        var orders = await _orderService.GetUserOrders(userid);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderFromUserCart([FromBody] OrderCreationDto orderCreationDto)
    {
        bool result = await _orderService.CreateFromUserCart(orderCreationDto);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromRoute] OrderStatus status)
    {
        bool result = await _orderService.ChangeStatus(id, status);

        if (!result)
            return NotFound();

        return Ok();
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> CancelOrder([FromRoute] Guid id)
    {
        bool result = await _orderService.CancelOrder(id);
        if (!result)
            return NotFound();

        return Ok();
    }
}
