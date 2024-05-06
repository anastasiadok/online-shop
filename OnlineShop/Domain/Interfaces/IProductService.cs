using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Dtos;
using Sieve.Models;

namespace OnlineShop.Domain.Interfaces;

public interface IProductService
{
    Task Add(ProductCreationDto productCreationDto);
    Task<IEnumerable<ProductDto>> GetCategoryProducts(Guid categoryId);
    Task<ProductDto> GetById(Guid id);
    Task<IEnumerable<ProductDto>> GetProductsByFilter([FromBody] SieveModel sieveModel);
    Task ChangeCategory(Guid id, Guid categoryId);
    Task<IEnumerable<ProductDto>> GetAll();
}