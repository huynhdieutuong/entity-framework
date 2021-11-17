﻿using System;
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

        static void Main(string[] args)
        {
            CreateDatabase();
        }
    }
}
