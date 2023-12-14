using FluentValidation;
using OnlineShop.Data.Models;

namespace OnlineShop.Domain.Validators;

public class CartItemDtoValidator : AbstractValidator<CartItem>
{
    public CartItemDtoValidator()
    {
        RuleFor(i => i.Quantity).GreaterThanOrEqualTo(1);
    }
}