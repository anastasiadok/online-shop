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

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById([FromRoute] Guid id)
    {
        var userDto = await _userService.GetById(id);
        return Ok(userDto);
    }

    [HttpPut]
    public async Task<IActionResult> ChageUserInfo([FromBody] UserDto userDto)
    {
        bool result = await _userService.ChangeUserInfo(userDto);

        if (!result)
            return BadRequest();

        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] UserCreationDto userDto)
    {
        bool result = await _userService.Create(userDto);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
}
