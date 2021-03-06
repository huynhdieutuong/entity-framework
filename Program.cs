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
            using var dbContext = new ShopContext();
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
            using var dbContext = new ShopContext();
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
        static void InsertData()
        {
            using var dbContext = new ShopContext();

            Category c1 = new Category() { Name = "Phone", Description = "Types of phone" };
            Category c2 = new Category() { Name = "Laptop", Description = "Types of laptop" };
            dbContext.categories.Add(c1);
            dbContext.categories.Add(c2);

            // var c1 = (from c in dbContext.categories where c.CategoryId == 1 select c).FirstOrDefault();
            // var c2 = (from c in dbContext.categories where c.CategoryId == 2 select c).FirstOrDefault();
            dbContext.Add(new Product() { Name = "Iphone X", Price = 1000, CateId = 1 });
            dbContext.Add(new Product() { Name = "Samsung Note 10", Price = 800, Category = c1 });
            dbContext.Add(new Product() { Name = "Dell XPS", Price = 1800, Category = c2 });
            dbContext.Add(new Product() { Name = "Hp Pavilion", Price = 600, Category = c2 });
            dbContext.Add(new Product() { Name = "Nokia 3310", Price = 100, Category = c1 });
            dbContext.Add(new Product() { Price = 500, Category = c2 });


            dbContext.SaveChanges();
        }
        static void ReadProduct(int productId)
        {
            using var dbContext = new ShopContext();
            var product = (from p in dbContext.products where p.ProductId == productId select p).FirstOrDefault();

            // using Proxies to automatically load Reference
            // var e = dbContext.Entry(product);
            // e.Reference(p => p.Category).Load(); // to get Category info (product.Category != null)

            product.PrintInfo();

            if (product.Category != null)
            {
                // Print Category info
                System.Console.WriteLine($"{product.Category.Name} - {product.Category.Description}");
            }
            else
            {
                System.Console.WriteLine("Category == null");
            }
        }
        static void ReadCategory(int categoryId)
        {
            using var dbContext = new ShopContext();
            var category = (from c in dbContext.categories where c.CategoryId == categoryId select c).FirstOrDefault();

            if (category == null)
            {
                System.Console.WriteLine("Category not found");
                return;
            }

            System.Console.WriteLine($"{category.CategoryId} - {category.Name}");

            // using Proxies to automatically load Collection
            // var e = dbContext.Entry(category);
            // e.Collection(c => c.Products).Load(); // to get List products in category

            if (category.Products != null)
            {
                System.Console.WriteLine($"Count: {category.Products.Count}");
                category.Products.ForEach(p => p.PrintInfo());
            }
            else
            {
                System.Console.WriteLine("Products == null");
            }
        }
        static void DeleteCategory(int categoryId)
        {
            using var dbContext = new ShopContext();
            var category = (from c in dbContext.categories where c.CategoryId == categoryId select c).FirstOrDefault();
            if (category == null)
            {
                System.Console.WriteLine("Category not found");
                return;
            }
            dbContext.Remove(category);
            dbContext.SaveChanges();
        }
        static void Main(string[] args)
        {
            DropDatabase();
            CreateDatabase();
            InsertData();
            // ReadProduct(2);

            // ReadCategory(1);
            // DeleteCategory(1);

        }
    }
}
