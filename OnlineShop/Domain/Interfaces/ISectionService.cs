using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface ISectionService
{
    Task<IEnumerable<SectionDto>> GetAll();
    Task<SectionDto> GetById(Guid id);
    Task<bool> Add(SectionDto sectionDto);
    Task<bool> ChangeName(Guid id, string name);
}