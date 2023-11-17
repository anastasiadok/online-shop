using FluentValidation;
using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Validators;

public class BrandValidator : AbstractValidator<Brand>
{
    public BrandValidator()
    {
        RuleFor(b => b.Name).NotEmpty().MaximumLength(30);
        RuleFor(b => b.BrandId).NotEmpty();
    }
}