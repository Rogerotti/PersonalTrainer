using Framework.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.DataBaseContext
{
    public class MealContext : DbContext
    {
        public DbSet<Meal> Meal { get; set; }

        public DbSet<ProductMeal> ProductMeal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMeal>().HasKey(x => new { x.MealId, x.ProductId });
        }

        public MealContext(DbContextOptions<MealContext> options)
            : base(options)
        {
        }
    }
}
