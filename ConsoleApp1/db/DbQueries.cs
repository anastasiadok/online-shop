using ConsoleApp1.db.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.db;

public class DbQueries
{
    public DbQueries(OnlineshopContext context)
    {
        db = context;
    }

    public OnlineshopContext db;

    public async Task<IEnumerable<Product>> GetAllBrandProducts(string brand)
    {
        return (await db.Brands
            .Where(b => b.Name == brand)
            .Include(b => b.Products)
            .FirstAsync())
            .Products.ToList();
    }

    public async Task<IEnumerable<ProductVariant>> GetAllProdVariants(Guid prodId)
    {
        return (await db.Products
            .Where(p => p.ProductId == prodId)
            .Include(p => p.ProductVariants)
            .FirstAsync())
            .ProductVariants
            .OrderBy(p => p.Product.Name)
            .ThenBy(p => p.Color)
            .ToList();
    }

    public async Task<Dictionary<Brand, int>> GetAllBrandsProductsNumber()
    {
        Dictionary<Brand, int> d = new();
        await db.Brands
            .Include(b => b.Products)
            .ForEachAsync(b => d.Add(b, b.Products.Count));

        return d;
    }

    public async Task<IEnumerable<Product>> GetAllSubcategoryProducts(Guid subcategory)
    {
        return (await db.Subcategories
            .Include(s => s.Products)
            .Where(s => s.SubcategoryId == subcategory)
            .FirstAsync())
            .Products.ToList();
    }

    public async Task<IEnumerable<Order>> GetAllCompletedOrdersWithProduct(Guid prodId)
    {
        return await db.OrderItems
            .Include(i=>i.Order)
            .Include(i=>i.ProductVariant)
            .Where(i=>i.ProductVariant.ProductId== prodId && i.Order.Status == TransactionStatus.Completed)
            .Select(i => i.Order)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetProductReviews(Guid prodId)
    {
        return (await db.Products
            .Where(p => p.ProductId == prodId)
            .Include(p => p.Reviews)
            .FirstAsync())
            .Reviews.ToList();
    }
}
