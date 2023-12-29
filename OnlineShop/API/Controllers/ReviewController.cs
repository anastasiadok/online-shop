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

    [HttpGet("products/{productid}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetProductReviews([FromRoute] Guid productid)
    {
        var reviews = await _reviewService.GetProductReviews(productid);
        if (reviews is null)
            return NotFound();

        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ReviewDto reviewDto)
    {
        bool result = await _reviewService.Add(reviewDto);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpGet("users/{userid}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetUserReviews([FromRoute] Guid userid)
    {
        var reviews = await _reviewService.GetUserReviews(userid);

        if (reviews is null)
            return NotFound();

        return Ok(reviews);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetById([FromRoute] Guid id)
    {
        var review = await _reviewService.GetById(id);

        if (review is null)
            return NotFound();

        return Ok(review);
    }


    [HttpGet]
    public async Task<ActionResult<ReviewDto>> GetAll()
    {
        var reviews = await _reviewService.GetAll();
        return Ok(reviews);
    }
}
