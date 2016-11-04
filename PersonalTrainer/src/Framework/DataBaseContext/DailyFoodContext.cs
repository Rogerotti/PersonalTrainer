using Framework.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Framework.DataBaseContext
{
    public class DailyFoodContext : DbContext
    {
        public DbSet<Product> Product { get; set; }

        public DbSet<ProductDetails> ProductsDetails { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserDetails> UsersDetails { get; set; }

        public DbSet<DailyFood> DailyFood { get; set; }

        public DbSet<DailyFoodProduct> DailyFoodProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyFoodProduct>().HasKey(x => new { x.DailyFoodId, x.ProductId });

            modelBuilder.Entity<DailyFood>()
              .HasMany(t => t.DailyFoodId)
              .WithMany(t => t.Courses)
              .Map(m =>
              {
                  m.ToTable("CourseInstructor");
                  m.MapLeftKey("CourseID");
                  m.MapRightKey("InstructorID");
              });
        }

        public DailyFoodContext(DbContextOptions<DailyFoodContext> options)
            : base(options)
        {
        }
    }
}
