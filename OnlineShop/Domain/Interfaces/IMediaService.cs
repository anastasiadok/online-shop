using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IMediaService
{
    Task<IEnumerable<MediaDto>> GetProductMedia(Guid productId);
    Task<bool> Add(MediaCreationDto mediaCreationDto);
    Task<bool> RemoveById(Guid id);
}