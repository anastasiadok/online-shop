using FluentValidation;
using OnlineShop.Domain.Dtos;
using System.Text.RegularExpressions;

namespace OnlineShop.Domain.Validators;

public class UserCreationDtoValidator : AbstractValidator<UserCreationDto>
{
    public UserCreationDtoValidator()
    {
        RuleFor(u => u.Password).NotEmpty().MinimumLength(8).MaximumLength(225);
        RuleFor(u => u.Email).MaximumLength(40).EmailAddress();
        RuleFor(u => u.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(u => u.LastName).NotEmpty().MaximumLength(50);
        RuleFor(u => u.Phone).MaximumLength(20).Matches(new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$"));
    }
}