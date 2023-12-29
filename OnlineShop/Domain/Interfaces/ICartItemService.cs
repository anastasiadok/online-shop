using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface ICartItemService
{
    Task<CartItemDto> GetById(Guid id);
    Task<IEnumerable<CartItemDto>> GetAll();
    Task<IEnumerable<CartItemDto>> GetUserCartItems(Guid userId);
    Task Add(CartItemDto cartItem);
    Task RemoveByKey(Guid userId, Guid productVariantId);
    Task ChangeQuantity(Guid userId, Guid productVariantId, int quantity);
}