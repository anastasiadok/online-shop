using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById([FromRoute] Guid id)
    {
        var brand = await _orderService.GetById(id);
        return Ok(brand);
    }

    [AllowAnonymous]
    [HttpGet("users/{userid}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders(Guid userid)
    {
        var orders = await _orderService.GetUserOrders(userid);
        return Ok(orders);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrderFromUserCart([FromBody] OrderCreationDto orderCreationDto)
    {
        await _orderService.CreateFromUserCart(orderCreationDto);
        return Ok();
    }

    [Authorize]
    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromRoute] OrderStatus status)
    {
        await _orderService.ChangeStatus(id, status);
        return Ok();
    }

    [Authorize]
    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> CancelOrder([FromRoute] Guid id)
    {
        await _orderService.CancelOrder(id);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<OrderDto>> GetAll()
    {
        var orders = await _orderService.GetAll();
        return Ok(orders);
    }
}
