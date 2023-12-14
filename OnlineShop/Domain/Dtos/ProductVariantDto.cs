namespace OnlineShop.Domain.Dtos;

public record ProductVariantDto : ProductVariantCreationDto
{
    public ColorDto ColorDto { get; set; }

    public SizeDto SizeDto { get; set; }
    public ProductVariantDto(Guid ProductVariantId, Guid ColorId, Guid SizeId, Guid ProductId, int Quantity, string Sku, SizeDto SizeDto, ColorDto ColorDto) : base(ProductVariantId, ColorId, SizeId, ProductId, Quantity, Sku)
    {
        this.ColorDto = ColorDto;
        this.SizeDto = SizeDto;
    }
}
