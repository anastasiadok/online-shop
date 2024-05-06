using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> Login(string email, string password);
    Task Register(UserCreationDto userDto);
}
