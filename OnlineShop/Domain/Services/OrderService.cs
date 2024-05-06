using OnlineShop.Data.Models;
using OnlineShop.Data;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using Mapster;
using OnlineShop.Domain.Exceptions;

namespace OnlineShop.Domain.Services;

public class OrderService : BaseService, IOrderService
{
    public OrderService(OnlineshopContext context) : base(context) { }

    public async Task CreateFromUserCart(OrderCreationDto creationDto)
    {
        var user = await _context.Users
            .Where(u => u.UserId == creationDto.UserId)
            .Include(a => a.Addresses)
            .FirstOrDefaultAsync();

        if (user is null)
            throw new BadRequestException("User doen't exist");

        var cartItems = await _context.CartItems
            .Where(i => i.UserId == creationDto.UserId)
            .Include(ci => ci.ProductVariant)
            .ThenInclude(pv => pv.Product)
            .ToListAsync();

        if (cartItems.Count == 0)
            throw new BadRequestException("Cart is empty");

        var address = await _context.Addresses.FindAsync(creationDto.AddressId) ?? throw new BadRequestException("Adress doesn't exist");
        if (address.UserId != creationDto.UserId)
            throw new BadRequestException("Address doesn't belong to user");

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
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrders(Guid userId)
    {
        return await _context.Orders.Where(m => m.UserId == userId).ProjectToType<OrderDto>().ToListAsync();
    }

    public async Task<OrderDto> GetById(Guid id)
    {
        var order = await _context.Orders.FindAsync(id) ?? throw new NotFoundException("Order");
        return order.Adapt<OrderDto>();
    }

    public async Task ChangeStatus(Guid id, OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(id) ?? throw new NotFoundException("Order");

        if (order.Status == OrderStatus.Cancelled)
            throw new BadRequestException("Order is cancelled. You can't change its status");

        if (order.Status >= status)
            throw new BadRequestException("You can't downgrade order status");

        order.Status = status;
        await _context.SaveChangesAsync();
    }

    public async Task CancelOrder(Guid id)
    {
        var order = await _context.Orders.FindAsync(id) ?? throw new NotFoundException("Order");

        if (order.Status == OrderStatus.Completed)
            throw new BadRequestException("Order is completed. You can't cancel it");

        order.Status = OrderStatus.Cancelled;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        return await _context.Orders.ProjectToType<OrderDto>().ToListAsync();
    }
}