using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Interfaces;
using System.Text.Json;

namespace OnlineShop.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var b = _brandService.GetAll();
            return Ok(b);
        }

        [HttpGet("get")]
        public async Task<ActionResult<Brand>> Get(Guid id)
        {
            var b = await _brandService.Get(id);
            return Ok(b);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Brand brand)
        {
            await _brandService.Add(brand);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Brand brand)
        {
            await _brandService.Update(brand);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _brandService.Remove(id);
            return Ok();
        }
    }
}
