using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class ReviewService : BaseService, IReviewService
{
    public ReviewService(OnlineshopContext context) : base(context) { }

    public async Task Add(ReviewDto reviewDto)
    {
        var product = await _context.Products.Where(p => p.ProductId == reviewDto.ProductId).Include(p => p.Reviews).FirstOrDefaultAsync();
        if (product is null)
            throw new BadRequestException("Product doesn't exist");

        var user = await _context.Users.FindAsync(reviewDto.UserId) ?? throw new BadRequestException("User doesn't exist");

        var review = reviewDto.Adapt<Review>();
        review.ReviewId = Guid.NewGuid();

        _context.Reviews.Add(review);

        var productReviews = product.Reviews;
        var ratingSum = productReviews.Select(r => r.Rating).Sum();
        var reviewsCount = productReviews.Count;
        review.Product.AverageRating = (float)ratingSum / reviewsCount;

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ReviewDto>> GetAll()
    {
        return await _context.Reviews.ProjectToType<ReviewDto>().ToListAsync();
    }

    public async Task<ReviewDto> GetById(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id) ?? throw new NotFoundException("Review");
        return review.Adapt<ReviewDto>();
    }

    public async Task<IEnumerable<ReviewDto>> GetProductReviews(Guid productId)
    {
        var product = await _context.Products.Where(p => p.ProductId == productId).Include(p => p.Reviews).FirstOrDefaultAsync();
        if (product is null)
            return null;

        return product.Reviews.Select(r => r.Adapt<ReviewDto>());
    }

    public async Task<IEnumerable<ReviewDto>> GetUserReviews(Guid userId)
    {
        var user = await _context.Users.Where(p => p.UserId == userId).Include(p => p.Reviews).FirstOrDefaultAsync();
        if (user is null)
            return null;

        return user.Reviews.Select(r => r.Adapt<ReviewDto>());
    }
}