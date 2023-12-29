using OnlineShop.Data.Models;
using OnlineShop.Data;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace OnlineShop.Domain.Services;

public class OrderService : BaseService, IOrderService
{
    public OrderService(OnlineshopContext context) : base(context) { }

    public async Task<bool> CreateFromUserCart(OrderCreationDto creationDto)
    {
        var user = await _context.Users
            .Where(u => u.UserId == creationDto.UserId)
            .Include(a => a.Addresses)
            .FirstOrDefaultAsync();

        if (user is null)
            return false;

        var cartItems = await _context.CartItems
            .Where(i => i.UserId == creationDto.UserId)
            .Include(ci => ci.ProductVariant)
            .ThenInclude(pv => pv.Product)
            .ToListAsync();

        if (cartItems.Count == 0)
            return false;

        var address = await _context.Addresses.FindAsync(creationDto.AddressId);
        if (address is null || address.UserId != creationDto.UserId)
            return false;

        Order order = new()
        {
            AddressId = creationDto.AddressId,
            UserId = creationDto.UserId,
            OrderId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            TotalPrice = cartItems.Sum(i => i.Quantity * i.ProductVariant.Product.Price)
        };

        List<OrderItem> orderItems = new();
        foreach (var item in cartItems)
        {
            orderItems.Add(new()
            {
                ProductVariantId = item.ProductVariantId,
                Quantity = item.Quantity,
                OrderId = order.OrderId,
                Price = item.ProductVariant.Product.Price
            });
        }

        order.OrderItems = orderItems;
        await _context.Orders.AddAsync(order);
        _context.CartItems.RemoveRange(cartItems);
        _context.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrders(Guid userId)
    {
        var orderList = await _context.Orders.Where(m => m.UserId == userId).ToListAsync();
        return orderList.Select(m => m.Adapt<OrderDto>());
    }

    public async Task<OrderDto> GetById(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        return order?.Adapt<OrderDto>();
    }

    public async Task<bool> ChangeStatus(Guid id, OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order is null)
            return false;

        if (order.Status == OrderStatus.Cancelled)
            return false;

        if (order.Status >= status)
            return false;

        order.Status = status;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CancelOrder(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order is null)
            return false;

        if (order.Status == OrderStatus.Completed)
            return false;

        order.Status = OrderStatus.Cancelled;
        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        return await _context.Orders.Select(o => o.Adapt<OrderDto>()).ToListAsync();
    }
}