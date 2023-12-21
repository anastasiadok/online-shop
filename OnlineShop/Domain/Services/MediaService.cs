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

    public async Task<bool> Add(MediaCreationDto mediaCreationDto)
    {
        var media = mediaCreationDto.Adapt<Media>();
        media.MediaId = Guid.NewGuid();

        using (var memoryStream = new MemoryStream())
        {
            await mediaCreationDto.File.CopyToAsync(memoryStream);
            media.Bytes = memoryStream.ToArray();
        }

        media.FileName = mediaCreationDto.File.Name;
        media.FileType = mediaCreationDto.File.ContentType;

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