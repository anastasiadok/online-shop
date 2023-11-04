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

        public void UpdateOverEntityState(User user,string firstname)
        {
            db.Users.Entry(user).State = EntityState.Detached;
            user.FirstName = firstname;
            db.Users.Attach(user);
            db.Users.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void UpdateOverExicution(User user,string firstname)
        {
            db.Users.Where(u=>u==user)
                .ExecuteUpdate(u=>u.SetProperty(u=>u.FirstName,firstname));
        }

        public void AddReview(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            var prodreviews = db.Reviews.Where(r => r.Product == review.Product);
            var s = prodreviews.Select(r => r.Rating).Sum();
            var c = prodreviews.Count();
            review.Product.AverageRating=(float)s/c;
            db.Update(review.Product);
            db.SaveChanges();
        }
    }
}
