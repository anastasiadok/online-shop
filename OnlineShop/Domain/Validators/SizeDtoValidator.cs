using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class SizeDtoValidator : AbstractValidator<SizeDto>
{
    public SizeDtoValidator()
    {
        RuleFor(c => c.SizeName).NotEmpty().MaximumLength(10);
    }
}