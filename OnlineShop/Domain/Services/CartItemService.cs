using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class CartItemService : BaseService, ICartItemService
{
    public CartItemService(OnlineshopContext context) : base(context) { }

    public async Task<CartItemDto> GetById(Guid id)
    {
        var item = await _context.CartItems.FindAsync(id) ?? throw new NotFoundException("Cart item");
        return item.Adapt<CartItemDto>();
    }

    public async Task<IEnumerable<CartItemDto>> GetAll()
    {
        return await _context.CartItems.ProjectToType<CartItemDto>().ToListAsync();
    }

    public async Task Add(CartItemDto cartItem)
    {
        await _context.CartItems.AddAsync(cartItem.Adapt<CartItem>());
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CartItemDto>> GetUserCartItems(Guid userId)
    {
        return await _context.CartItems.Where(i => i.UserId == userId).ProjectToType<CartItemDto>().ToListAsync();
    }

    public async Task RemoveByKey(Guid userId, Guid productVariantId)
    {
        var item = await _context.CartItems.FindAsync(new { userId, productVariantId }) ?? throw new NotFoundException("Cart item");

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeQuantity(Guid userId, Guid productVariantId, int quantity)
    {
        var item = await _context.CartItems.FindAsync(new { userId, productVariantId }) ?? throw new NotFoundException("Cart item");

        item.Quantity = quantity;
        await _context.SaveChangesAsync();
    }
}