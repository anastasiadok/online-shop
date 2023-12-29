using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[AllowAnonymous]
[Route("api/addresses")]
[ApiController]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddressDto>> GetById([FromRoute] Guid id)
    {
        var address = await _addressService.GetById(id);
        return Ok(address);
    }

    [HttpGet]
    public async Task<ActionResult<AddressDto>> GetAll()
    {
        var addresses = await _addressService.GetAll();
        return Ok(addresses);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddressDto addressDto)
    {
        await _addressService.Add(addressDto);
        return Ok();
    }
}
