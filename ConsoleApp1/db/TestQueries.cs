using ConsoleApp1.db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.db
{
    public class TestQueries
    {
        public TestQueries(DbQueries dbq)
        {
            q = dbq;
        }
        DbQueries q;

        public void Test1()
        {
            foreach (var b in q.db.Brands.Select(b => b.Name).ToList())
            {
                var res = q.GetAllBrandProducts(b);
                Console.WriteLine(b + " " + res.Count());
                foreach (var v in res)
                {
                    Console.WriteLine(v.Name);
                }
            }
        }

        public void Test2()
        {
            foreach (var b in q.db.Products.Select(b => b.ProductId).ToList())
            {
                var res = q.GetAllProdVariants(b);
                Console.WriteLine(q.db.Products.Where(p => p.ProductId == b).First().Name + " " + res.Count());
                foreach (var v in res)
                {
                    Console.WriteLine(v.Product.Name);
                }
            }
        }

        public void Test3()
        {
            var res = q.GetAllBrandsProductsNumber();
            foreach (var v in res)
            {
                Console.WriteLine(v.Key.Name + " " + v.Value);
            }
        }

        public void Test4()
        {
            foreach (var s in q.db.Subcategories.Select(s => s.SubcategoryId).ToList())
            {
                var res = q.GetAllSubcategoryProducts(s);
                Console.WriteLine(q.db.Subcategories.Where(u => u.SubcategoryId == s).First().Name + " " + res.Count());
                foreach (var v in res)
                {
                    Console.WriteLine(v.Name);
                }
            }
        }

        public void Test5()
        {
            foreach (var b in q.db.Products.Select(b => b.ProductId).ToList())
            {
                var res = q.GetAllCompletedOrdersWithProduct(b);
                Console.WriteLine(q.db.Products.Where(p => p.ProductId == b).First().Name + " " + res.Count());
                foreach (var v in res)
                {
                    Console.WriteLine(v.TotalPrice);
                }
            }
        }

        public void Test6()
        {
            foreach (var b in q.db.Products.Select(b => b.ProductId).ToList())
            {
                var res = q.GetProductReviews(b);
                Console.WriteLine(q.db.Products.Where(p => p.ProductId == b).First().Name + " " + res.Count());
                foreach (var v in res)
                {
                    Console.WriteLine(v.CommentText +" "+ v.Rating);
                }
            }
        }
    }
}
