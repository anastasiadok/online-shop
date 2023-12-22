using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetProductReviews(Guid productId);
    Task<IEnumerable<ReviewDto>> GetUserReviews(Guid userId);
    Task<bool> Add(ReviewDto reviewDto);
    Task<ReviewDto> GetById(Guid id);
    Task<IEnumerable<ReviewDto>> GetAll();
}