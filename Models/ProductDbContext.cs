using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> products { get; set; }
        private const string connectionString = "Server=TUONG\\SQLEXPRESS;Database=data01;Trusted_Connection=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}