using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IOrderService
{
    Task CreateFromUserCart(OrderCreationDto creationDto);
    Task<IEnumerable<OrderDto>> GetUserOrders(Guid userId);
    Task<OrderDto> GetById(Guid id);
    Task ChangeStatus(Guid id, OrderStatus status);
    Task CancelOrder(Guid id);
    Task<IEnumerable<OrderDto>> GetAll();
}