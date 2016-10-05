using Framework.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Framework.DataBaseContext
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserDetails> UsersDetails{ get;set;}

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
    }
}
