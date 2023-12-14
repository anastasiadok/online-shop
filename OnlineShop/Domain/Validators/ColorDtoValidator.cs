using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class ColorDtoValidator : AbstractValidator<ColorDto>
{
    public ColorDtoValidator()
    {
        RuleFor(c => c.ColorName).NotEmpty().MaximumLength(20);
    }
}