using FluentValidation;
using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Validators;

public class SectionDtoValidator : AbstractValidator<Section>
{
    public SectionDtoValidator()
    {
        RuleFor(s => s.Name).NotEmpty().MaximumLength(20);
    }
}