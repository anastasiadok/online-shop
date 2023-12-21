using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

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
    public async Task<ActionResult<IEnumerable<MediaDto>>> Get([FromRoute] Guid productid)
    {
        var mediaDtoList = await _mediaService.GetProductMedia(productid);
        return Ok(mediaDtoList);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] MediaCreationDto mediaCreationDto)
    {
        bool result = await _mediaService.Add(mediaCreationDto);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        bool result = await _mediaService.RemoveById(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}
