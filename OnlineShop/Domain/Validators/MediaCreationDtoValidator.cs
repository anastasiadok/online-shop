using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class MediaCreationDtoValidator : AbstractValidator<MediaDto>
{
    public MediaCreationDtoValidator()
    {
        RuleFor(m => m.Bytes).NotEmpty();
    }
}