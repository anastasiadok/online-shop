using ConsoleApp1.db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.db
{
    public class Crud
    {
        public Crud(OnlineshopContext context)
        {
            db = context;
        }

        readonly OnlineshopContext db;

        public async Task UpdateOverEntityState(User user,string firstname)
        {
            db.Users.Entry(user).State = EntityState.Detached;
            user.FirstName = firstname;
            db.Users.Attach(user);
            db.Users.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task UpdateOverExicution(User user,string firstname)
        {
            await db.Users.Where(u=>u==user)
            .ExecuteUpdateAsync(u=>u.SetProperty(u=>u.FirstName,firstname));
        }

        public async Task AddReview(Review review)
        {
            db.Reviews.Add(review);
            await db.SaveChangesAsync();
            var prodreviews = db.Reviews.Where(r => r.Product == review.Product);
            var s = prodreviews.Select(r => r.Rating).Sum();
            var c = prodreviews.Count();
            review.Product.AverageRating=(float)s/c;
            db.Update(review.Product);
            await db.SaveChangesAsync();
        }
    }
}
