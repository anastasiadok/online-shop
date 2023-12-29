using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Domain.Services;

public class UserService : BaseService, IUserService
{
    public UserService(OnlineshopContext context) : base(context) { }

    public async Task<bool> ChangeUserInfo(UserDto userDto)
    {
        var user = await _context.Users.FindAsync(userDto.UserId);
        if (user == null)
            return false;

        user.LastName = userDto.LastName;
        user.FirstName = userDto.FirstName;
        user.Email = userDto.Email;
        user.Phone = userDto.Phone;
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> Create(UserCreationDto userDto)
    {
        if (userDto is null)
            return false;

        var user = userDto.Adapt<User>();
        user.UserId = Guid.NewGuid();
        user.Password = new PasswordHasher<User>().HashPassword(user, userDto.Password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        return user?.Adapt<UserDto>();
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        return await _context.Users.Select(pv => pv.Adapt<UserDto>()).ToListAsync();
    }
}
