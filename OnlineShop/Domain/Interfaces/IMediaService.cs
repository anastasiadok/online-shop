using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IMediaService
{
    Task<IEnumerable<MediaDto>> GetProductMedia(Guid productId);
    Task Add(MediaCreationDto mediaCreationDto);
    Task RemoveById(Guid id);

    Task<MediaDto> GetById(Guid id);

    Task<IEnumerable<MediaDto>> GetAll();
}