using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IMediaService
{
    Task<IEnumerable<MediaDto>> GetProductMedia(Guid productId);
    Task<bool> Add(MediaDto mediaDto);
    Task<bool> RemoveById(Guid id);
}