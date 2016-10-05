using Framework.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Framework.DataBaseContext
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductDetails> ProductsDetails { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }
    }
}
