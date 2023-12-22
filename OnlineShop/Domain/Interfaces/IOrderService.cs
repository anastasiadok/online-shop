using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IOrderService
{
    Task<bool> CreateFromUserCart(OrderCreationDto creationDto);
    Task<IEnumerable<OrderDto>> GetUserOrders(Guid userId);
    Task<OrderDto> GetById(Guid id);
    Task<bool> ChangeStatus(Guid id, OrderStatus status);
    Task<bool> CancelOrder(Guid id);
    Task<IEnumerable<OrderDto>> GetAll();
}