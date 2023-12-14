using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OnlineShop.Domain.Dtos;
using Sieve.Models;
using Sieve.Services;

namespace OnlineShop.Domain;

public class ApplicationSieveProcessor : SieveProcessor
{
    public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options) { }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.Property<ProductDto>(p => p.AverageRating).CanFilter().CanSort();

        mapper.Property<ProductDto>(p => p.Price).CanFilter().CanSort();

        mapper.Property<ProductDto>(p => p.CategoryId).CanFilter();

        mapper.Property<ProductDto>(p=>p.BrandId).CanFilter();

        mapper.Property<ProductDto>(p => p.ProductVariants.Select(pv => pv.ColorId)).CanFilter();

        mapper.Property<ProductDto>(p => p.ProductVariants.Select(pv => pv.SizeId)).CanFilter();

        return mapper;
    }
}