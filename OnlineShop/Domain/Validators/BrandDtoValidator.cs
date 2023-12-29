using FluentValidation;
using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Validators;

public class BrandDtoValidator : AbstractValidator<Brand>
{
    public BrandDtoValidator()
    {
        RuleFor(b => b.Name).NotEmpty().MaximumLength(30);
    }
}