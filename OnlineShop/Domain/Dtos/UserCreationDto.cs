using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public record UserCreationDto(UserType Role, string Password, string Email, string Phone, string FirstName, string LastName);