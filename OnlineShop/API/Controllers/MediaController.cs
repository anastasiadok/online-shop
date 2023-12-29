using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[AllowAnonymous]
[Route("api/media")]
[ApiController]
public class MediaController : Controller
{
    private readonly IMediaService _mediaService;
    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [HttpGet("products/{productid}")]
    public async Task<ActionResult<IEnumerable<MediaDto>>> GetByProduct([FromRoute] Guid productid)
    {
        var mediaDtoList = await _mediaService.GetProductMedia(productid);
        return Ok(mediaDtoList);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] MediaCreationDto mediaCreationDto)
    {
        await _mediaService.Add(mediaCreationDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediaService.RemoveById(id);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MediaDto>> GetById([FromRoute] Guid id)
    {
        var media = await _mediaService.GetById(id);
        return Ok(media);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MediaDto>>> GetAll()
    {
        var media = await _mediaService.GetAll();
        return Ok(media);
    }
}
