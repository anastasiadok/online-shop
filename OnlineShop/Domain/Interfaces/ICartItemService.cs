using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface ICartItemService
{
    Task<IEnumerable<CartItemDto>> GetUserCartItems(Guid userId);
    Task<bool> Add(CartItemDto cartItem);
    Task<bool> RemoveByKey(Guid userId, Guid productVariantId);
    Task<bool> ChangeQuantity(Guid userId, Guid productVariantId, int quantity);
}