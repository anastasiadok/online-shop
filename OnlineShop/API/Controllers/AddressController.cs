using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services;
using System.Net;

namespace OnlineShop.API.Controllers;

[Route("api/addresses")]
[ApiController]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService)
    {
        _addressService=addressService; 
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddressDto>> Get([FromRoute] Guid id)
    {
        var address = await _addressService.GetById(id);

        if (address is null)
            return NotFound();

        return Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddressDto addressDto)
    {
        bool result = await _addressService.Add(addressDto);

        if (!result)
            return BadRequest();

        return Ok();
    }
}
