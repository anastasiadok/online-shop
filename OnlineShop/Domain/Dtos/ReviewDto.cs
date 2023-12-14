namespace OnlineShop.Domain.Dtos;

public record ReviewDto(Guid ReviewId, Guid ProductId, Guid UserId, int Rating, string? CommentText, string? Title, DateTime CreatedAt);