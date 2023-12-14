using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services;
using Sieve.Services;
using System.Reflection;

namespace OnlineShop;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<OnlineshopContext>(options => options.UseNpgsql(connection));

        builder.Services.AddControllers();
        builder.Services.AddFluentValidation(v => v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        builder.Services.AddScoped<IAddressService, AddressService>();
        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<ICartItemService, CartItemService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IColorService, ColorService>();
        builder.Services.AddScoped<IMediaService, MediaService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IProductVariantService, ProductVariantService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<ISectionService, SectionService>();
        builder.Services.AddScoped<ISizeService, SizeService>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<SieveProcessor>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        //app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}