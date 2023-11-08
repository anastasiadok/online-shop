using ConsoleApp1.db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.db
{
    public class DbQueries
    {
        public DbQueries(OnlineshopContext context)
        {
            db = context;
        }

        public OnlineshopContext db;

        public async Task<IEnumerable<Product>> GetAllBrandProducts(string brand)
        {
            var l = await db.Brands
                .Where(b => b.Name == brand)
                .Include(b => b.Products)
                .FirstAsync();

            return l.Products.ToList();
        }

        public async Task<IEnumerable<ProductVariant>> GetAllProdVariants(Guid prodId)
        {
            var l = await db.Products
                .Where(p => p.ProductId == prodId)
                .Include(p => p.ProductVariants)
                .FirstAsync();

            return l.ProductVariants
                .OrderBy(p => p.Product.Name)
                .ThenBy(p => p.Color)
                .ToList();
        }

        public async Task<Dictionary<Brand, int>> GetAllBrandsProductsNumber()
        {
            Dictionary<Brand, int> d = new();
            await db.Brands.Include(b => b.Products)
                .ForEachAsync(b => d.Add(b, b.Products.Count));

            return d;
        }

        public async Task<IEnumerable<Product>> GetAllSubcategoryProducts(Guid subcategory)
        {
            var l = await db.Subcategories
                .Include(s => s.Products)
                .Where(s => s.SubcategoryId == subcategory)
                .FirstAsync();

            return l.Products.ToList();
        }

        public async Task<IEnumerable<Order>> GetAllCompletedOrdersWithProduct(Guid prodId)
        {
            var p = db.Products
                .Where(p => p.ProductId == prodId)
                .Include(p => p.ProductVariants)
                .ThenInclude(v => v.OrderItems)
                .ThenInclude(i => i.Order)
                .AsSplitQuery();

            var l = p
                .SelectMany(p => p.ProductVariants)
                .SelectMany(p => p.OrderItems)
                .Select(i => i.Order);

            var res = await l
                .Where(o => o.Status == TransactionStatus.Completed)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();

            return res;
        }

        public async Task<IEnumerable<Review>> GetProductReviews(Guid prodId)
        {
            var l = await db.Products
                .Where(p => p.ProductId == prodId)
                .Include(p => p.Reviews)
                .FirstAsync();

            return l.Reviews.ToList();
        }
    }
}
