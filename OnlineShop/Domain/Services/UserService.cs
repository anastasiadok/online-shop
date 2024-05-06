using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class UserService : BaseService, IUserService
{
    public UserService(OnlineshopContext context) : base(context) { }

    public async Task ChangeUserInfo(UserDto userDto)
    {
        var user = await _context.Users.FindAsync(userDto.UserId) ?? throw new NotFoundException("User");

        user.LastName = userDto.LastName;
        user.FirstName = userDto.FirstName;
        user.Email = userDto.Email;
        user.Phone = userDto.Phone;

        _context.SaveChanges();
    }

    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new NotFoundException("User");
        return user.Adapt<UserDto>();
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        return await _context.Users.ProjectToType<UserDto>().ToListAsync();
    }
}
