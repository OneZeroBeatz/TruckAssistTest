using Microsoft.EntityFrameworkCore;

namespace RestAPI.Context
{
    public class RestApiDbContext : DbContext
    {
        public RestApiDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
