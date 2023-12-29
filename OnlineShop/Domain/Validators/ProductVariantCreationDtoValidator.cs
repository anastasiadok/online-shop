using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class ProductVariantCreationDtoValidator : AbstractValidator<ProductVariantCreationDto>
{
    public ProductVariantCreationDtoValidator()
    {
        RuleFor(p => p.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(p => p.Sku).NotEmpty();
    }
}