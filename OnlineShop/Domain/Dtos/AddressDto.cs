using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Dtos;

public record AddressDto(Guid AddressId, Guid UserId, string Address1);
