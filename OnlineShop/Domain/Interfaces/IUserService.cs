using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IUserService
{
    Task<UserDto> GetById(Guid id);
    Task<bool> Create(UserCreationDto user);
    Task<bool> ChangeUserInfo(UserDto userDto);
    Task<IEnumerable<UserDto>> GetAll();
}