using FluentValidation;
using OnlineShop.Domain.Dtos;

namespace OnlineShop.Domain.Validators;

public class MediaDtoValidator : AbstractValidator<MediaDto>
{
    public MediaDtoValidator()
    {
        RuleFor(m => m.FileName).NotEmpty().MaximumLength(50);
        RuleFor(m => m.FileType).NotEmpty().MaximumLength(10);
        RuleFor(m => m.Bytes).NotEmpty();
    }
}