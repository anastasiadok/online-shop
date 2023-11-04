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

        public IEnumerable<Product> GetAllBrandProducts(string brand)
        {
            return db.Brands
                .Where(b => b.Name == brand)
                .Include(b => b.Products)
                .First().Products
                .ToList();
        }

        public IEnumerable<ProductVariant> GetAllProdVariants(Guid prodId)
        {
            return db.Products
                .Where(p => p.ProductId == prodId)
                .Include(p => p.ProductVariants)
                .First().ProductVariants
                .ToList();
        }

        public Dictionary<Brand,int> GetAllBrandsProductsNumber()
        {
            Dictionary<Brand, int> d = new();
            var brands= db.Brands.Include(b=>b.Products).ToList();
            foreach( var brand in brands )
            {
                d.Add(brand, brand.Products.Count);
            }
            return d;
        }

        public IEnumerable<Product> GetAllSubcategoryProducts(Guid subcategory)
        {
            var l = db.Subcategories.Include(s=>s.Products).Where(s=>s.SubcategoryId== subcategory).First();
            return l.Products.ToList();
        }

        public IEnumerable<Order> GetAllCompletedOrdersWithProduct(Guid prodId)
        {
            var p= db.Products.Where(p=>p.ProductId==prodId).Include(p => p.ProductVariants).ThenInclude(v => v.OrderItems).ThenInclude(i => i.Order).AsSplitQuery().ToList();
            var l = p.SelectMany(p => p.ProductVariants).SelectMany(p=>p.OrderItems).Select(i=>i.Order);
            return l.Where(o=>o.Status==TransactionStatus.Completed);
        }

        public IEnumerable<Review> GetProductReviews(Guid prodId)
        {
            return db.Products.Where(p => p.ProductId == prodId).Include(p => p.Reviews).First().Reviews.ToList();
        }
    }
}
