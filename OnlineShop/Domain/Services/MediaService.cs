using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class MediaService : BaseService, IMediaService
{
    public MediaService(OnlineshopContext context) : base(context) { }

    public async Task<IEnumerable<MediaDto>> GetProductMedia(Guid productId)
    {
        var mediaList = await _context.Media.Where(m => m.ProductId == productId).ToListAsync();
        return mediaList.Select(m => m.Adapt<MediaDto>());
    }

    public async Task<bool> Add(MediaDto mediaDto)
    {
        var media = mediaDto.Adapt<Media>();
        media.MediaId = Guid.NewGuid();

        await _context.Media.AddAsync(media);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveById(Guid id)
    {
        var media = await _context.Media.FindAsync(id);

        if (media is null)
            return false;

        _context.Media.Remove(media);
        await _context.SaveChangesAsync();

        return true;
    }
}