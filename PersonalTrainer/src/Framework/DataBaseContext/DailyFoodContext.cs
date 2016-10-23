using Framework.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Framework.DataBaseContext
{
    public class DailyFoodContext : DbContext
    {
        public DbSet<DailyFood> DailyFood { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<DailyFoodProduct> DailyFoodProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyFoodProduct>().HasKey(x => new { x.DailyFoodId, x.ProductId });
        }

        public DailyFoodContext(DbContextOptions<DailyFoodContext> options)
            : base(options)
        {
        }
    }
}
