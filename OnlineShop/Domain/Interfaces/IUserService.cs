using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IUserService
{
    Task<UserDto> GetById(Guid id);
    Task ChangeUserInfo(UserDto userDto);
    Task<IEnumerable<UserDto>> GetAll();
}