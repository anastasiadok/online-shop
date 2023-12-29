using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class CartItemService : BaseService, ICartItemService
{
    public CartItemService(OnlineshopContext context) : base(context) { }

    public async Task<CartItemDto> GetById(Guid id)
    {
        var item = await _context.CartItems.FindAsync(id);
        return item?.Adapt<CartItemDto>();
    }

    public async Task<IEnumerable<CartItemDto>> GetAll()
    {
        return await _context.CartItems.Select(a => a.Adapt<CartItemDto>()).ToListAsync();
    }

    public async Task<bool> Add(CartItemDto cartItem)
    {
        await _context.CartItems.AddAsync(cartItem.Adapt<CartItem>());
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<CartItemDto>> GetUserCartItems(Guid userId)
    {
        var items = await _context.CartItems
            .Where(i => i.UserId == userId)
            .ToListAsync();

        return items.Select(i => i.Adapt<CartItemDto>());
    }

    public async Task<bool> RemoveByKey(Guid userId, Guid productVariantId)
    {
        var item = await _context.CartItems.FindAsync(new { userId, productVariantId });

        if (item is null)
            return false;

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeQuantity(Guid userId, Guid productVariantId, int quantity)
    {
        var item = await _context.CartItems.FindAsync(new { userId, productVariantId });

        if (item is null)
            return false;

        item.Quantity = quantity;
        await _context.SaveChangesAsync();

        return true;
    }
}