using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface ISectionService
{
    Task<IEnumerable<SectionDto>> GetAll();
    Task<SectionDto> GetById(Guid id);
    Task Add(SectionDto sectionDto);
    Task ChangeName(Guid id, string name);
}