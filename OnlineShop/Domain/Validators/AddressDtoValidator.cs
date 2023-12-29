using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(a => a.Address1).NotEmpty().MaximumLength(100);
    }
}
