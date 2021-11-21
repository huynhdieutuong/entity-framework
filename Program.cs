using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new WebContext();
            dbContext.Database.EnsureCreated();
        }
    }
}
