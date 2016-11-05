using Framework.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace Framework.DataBaseContext
{
    public class DefaultContext : DbContext
    {
        public DbSet<Product> Product { get; set; }

        public DbSet<ProductDetails> ProductsDetails { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserDetails> UsersDetails { get; set; }

        public DbSet<DayFoodDiary> DailyFood { get; set; }

        public DbSet<DiaryProduct> DailyFoodProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiaryProduct>().HasKey(x => new { x.DayId, x.ProductId });

            foreach (IMutableForeignKey mutableForeignKey in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                mutableForeignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }
    }
}
