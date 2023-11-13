using ConsoleApp1.db;
using ConsoleApp1.db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new OnlineshopContext())
            {
                DbQueries q = new(db);
                TestQueries test = new(q);
                test.Test2();
            }
        }
    }
}