using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFramework
{
    public class ShopContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();
        });

        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        private const string connectionString = "Server=TUONG\\SQLEXPRESS;Database=shopdata;Trusted_Connection=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
            // optionsBuilder.UseLazyLoadingProxies(); // if use lazyloading to load all Reference, Collection, the system will be heavy
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API = Attribute

            // using entity to call Fluent Api for Product (entity.FluentApi):
            // Way 1. var entity = modelBuilder.Entity(typeof(Product));
            // Way 2. var entity = modelBuilder.Entity<Product>();
            // Way 3.
            modelBuilder.Entity<Product>(entity =>
            {
                // 1. Table mapping
                entity.ToTable("Product");

                // 2. Primary Key
                entity.HasKey(p => p.ProductId);

                // 3. Index (can not index by Attribute)
                entity.HasIndex(p => p.Price)
                      .HasDatabaseName("Product_Price"); // Naming Index

                // Relative 1 (Foreign Key)
                entity.HasOne(p => p.Category)
                      .WithMany()
                      .HasForeignKey("CateId") // 4. Naming Foreign Key
                      .OnDelete(DeleteBehavior.Cascade) // 5. Delete Rule: Cascade
                      .HasConstraintName("FK_Product_Category_CateId"); // 6. Naming Foreign Key Index

                // Relative 2
                entity.HasOne(p => p.Category2)
                      .WithMany(c => c.Products) // 7. Collect Navigation
                      .HasForeignKey("CateId2")
                      .OnDelete(DeleteBehavior.NoAction);

                // Override attributes of Name Property in Product Model
                entity.Property(p => p.Name)
                      .HasColumnName("Title")
                      .HasColumnType("nvarchar")
                      .HasMaxLength(60)
                      .IsRequired(false)
                      .HasDefaultValue("Default Name");
            });

        }
    }
}