using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        static void ReadProducts(int limit)
        {
            using var dbContext = new ProductDbContext();

            // var query = from product in dbContext.products
            //             where product.ProductId >= 3
            //             select product;
            var query = dbContext.products.Take(limit);

            query.ToList().ForEach(product => product.PrintInfo());
        }
        static void ReadProduct(int id)
        {
            using var dbContext = new ProductDbContext();
            var query = from p in dbContext.products
                        where p.ProductId == id
                        select p;
            var product = query.FirstOrDefault();
            if (product != null) product.PrintInfo();
        }
        static void UpdateProduct(int id, string newName = "", string newProvider = "")
        {
            var dbContext = new ProductDbContext();
            Product product = (from p in dbContext.products
                               where p.ProductId == id
                               select p).FirstOrDefault();
            if (product != null)
            {
                // Use Entry to prevent Update
                // EntityEntry<Product> entry = dbContext.Entry(product);
                // entry.State = EntityState.Detached;

                if (newName != "") product.ProductName = newName;
                if (newProvider != "") product.Provider = newProvider;

                dbContext.SaveChanges();
                ReadProduct(product.ProductId);
            }
            else
            {
                System.Console.WriteLine("Product not found");
            }
        }
        static void Main(string[] args)
        {
            // CreateDatabase();
            // DropDatabase();

            // InsertProduct();

            // ReadProducts(3);
            // ReadProduct(5);

            // UpdateProduct(6, "Product 6", "Company 6");
            // UpdateProduct(7, "Product 7");
            // UpdateProduct(8, newProvider: "Company 8");
        }
    }
}
