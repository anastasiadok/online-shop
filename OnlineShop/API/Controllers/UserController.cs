using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;


[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById([FromRoute] Guid id)
    {
        var userDto = await _userService.GetById(id);
        return Ok(userDto);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> ChangeUserInfo([FromBody] UserDto userDto)
    {
        await _userService.ChangeUserInfo(userDto);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
}
