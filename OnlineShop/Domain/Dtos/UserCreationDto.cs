using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public record UserCreationDto : UserDto
{
    public UserType Role { get; set; }
    public string Password { get; set; }
    public UserCreationDto(Guid UserId, UserType Role, string Password, string Email, string Phone, string FirstName, string LastName)
        : base(UserId, Email, Phone, FirstName, LastName)
    {
        this.Role = Role;
        this.Password = Password;
    }
}