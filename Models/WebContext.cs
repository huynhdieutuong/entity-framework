using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFramework
{
    public class WebContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();
        });

        public DbSet<Article> articles { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        private const string connectionString = "Server=TUONG\\SQLEXPRESS;Database=webdb;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ArticleTag>(entity =>
            {
                // Create index for TagId, ArticleId to unique one pair (TagId, ArticleId):
                // Row1: TagId = 1, ArticleId = 1
                // Row2: TagId = 1, ArticleId = 1 (not unique)
                // Row3: TagId = 1, ArticleId = 2 (unique)
                entity.HasIndex(articleTags => new { articleTags.TagId, articleTags.ArticleId })
                      .IsUnique();
            });
        }
    }
}