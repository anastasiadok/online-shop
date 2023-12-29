using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class ProductCreationDtoValidator : AbstractValidator<ProductCreationDto>
{
    public ProductCreationDtoValidator()
    {
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
    }
}