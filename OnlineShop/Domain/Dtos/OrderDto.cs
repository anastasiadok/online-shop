using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public record OrderDto(Guid OrderId, Guid UserId, decimal TotalPrice, Guid AddressId,
    DateTime CreatedAt, OrderStatus Status);