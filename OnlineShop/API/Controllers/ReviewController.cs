using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.API.Controllers;

[Route("api/reviews")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;
    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [AllowAnonymous]
    [HttpGet("products/{productid}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetProductReviews([FromRoute] Guid productid)
    {
        var reviews = await _reviewService.GetProductReviews(productid);
        return Ok(reviews);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ReviewDto reviewDto)
    {
        await _reviewService.Add(reviewDto);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("users/{userid}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetUserReviews([FromRoute] Guid userid)
    {
        var reviews = await _reviewService.GetUserReviews(userid);
        return Ok(reviews);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetById([FromRoute] Guid id)
    {
        var review = await _reviewService.GetById(id);
        return Ok(review);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ReviewDto>> GetAll()
    {
        var reviews = await _reviewService.GetAll();
        return Ok(reviews);
    }
}
