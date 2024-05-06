using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class MediaService : BaseService, IMediaService
{
    public MediaService(OnlineshopContext context) : base(context) { }

    public async Task<IEnumerable<MediaDto>> GetProductMedia(Guid productId)
    {
        var product = await _context.Products.Where(p => p.ProductId == productId).Include(p=>p.Media).FirstOrDefaultAsync();
        if (product is null)
            throw new BadRequestException("Product doesn't exist");

        return product.Media.Select(m => m.Adapt<MediaDto>());
    }

    public async Task Add(MediaCreationDto mediaCreationDto)
    {
        _ = await _context.Products.FindAsync(mediaCreationDto.ProductId) ?? throw new BadRequestException("Product doesn't exist");

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
    }

    public async Task RemoveById(Guid id)
    {
        var media = await _context.Media.FindAsync(id) ?? throw new NotFoundException("Media");

        _context.Media.Remove(media);
        await _context.SaveChangesAsync();
    }

    public async Task<MediaDto> GetById(Guid id)
    {
        var media = await _context.Media.FindAsync(id) ?? throw new NotFoundException("Media");
        return media.Adapt<MediaDto>();
    }

    public async Task<IEnumerable<MediaDto>> GetAll()
    {
        return await _context.Media.ProjectToType<MediaDto>().ToListAsync();
    }
}