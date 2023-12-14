using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class UserCreationDtoValidator : UserDtoValidator<UserCreationDto>
{
    public UserCreationDtoValidator()
    {
        RuleFor(u => u.Password).NotEmpty().MinimumLength(8).MaximumLength(225);
    }
}