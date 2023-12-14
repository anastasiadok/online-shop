namespace OnlineShop.Domain.Dtos;

public record UserDto(Guid UserId, string Email, string Phone, string FirstName, string LastName);