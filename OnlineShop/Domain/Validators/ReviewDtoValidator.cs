using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class ReviewDtoValidator : AbstractValidator<ReviewDto>
{
    public ReviewDtoValidator()
    {
        RuleFor(r => r.CommentText).MaximumLength(2000);
        RuleFor(r => r.Title).MaximumLength(50);
        RuleFor(r => r.Rating).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
    }
}