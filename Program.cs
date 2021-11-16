using System;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    class Program
    {
        static void CreateDatabase()
        {
            using var dbContext = new ProductDbContext();
            string dbName = dbContext.Database.GetDbConnection().Database;

            bool result = dbContext.Database.EnsureCreated();
            if (result)
            {
                System.Console.WriteLine($"{dbName} was created.");
            }
            else
            {
                System.Console.WriteLine($"{dbName} was not created.");
            }
        }
        static void DropDatabase()
        {
            using var dbContext = new ProductDbContext();
            string dbName = dbContext.Database.GetDbConnection().Database;

            bool result = dbContext.Database.EnsureDeleted();
            if (result)
            {
                System.Console.WriteLine($"{dbName} was deleted.");
            }
            else
            {
                System.Console.WriteLine($"{dbName} was not deleted.");
            }
        }
        static void InsertProduct()
        {
            using var dbContext = new ProductDbContext();
            // var p1 = new Product();
            // p1.ProductName = "Product 1";
            // p1.Provider = "Company 1";
            // dbContext.Add(p1);

            // var p2 = new Product()
            // {
            //     ProductName = "Product 2",
            //     Provider = "Company 2"
            // };
            // dbContext.Add(p2);

            var products = new object[] {
                new Product() { ProductName = "Product 3", Provider = "Company 3" },
                new Product() { ProductName = "Product 4", Provider = "Company 4" },
                new Product() { ProductName = "Product 5", Provider = "Company 5" },
            };
            dbContext.AddRange(products);

            // Save
            int numberRows = dbContext.SaveChanges();
            System.Console.WriteLine($"Inserted {numberRows} rows");
        }
        static void Main(string[] args)
        {
            // CreateDatabase();
            // DropDatabase();

            InsertProduct();
        }
    }
}
