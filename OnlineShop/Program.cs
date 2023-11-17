
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Data;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services;
using OnlineShop.Domain.Validators;

namespace OnlineShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BrandValidator>());
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<OnlineshopContext>(options => options.UseNpgsql(connection));


            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}