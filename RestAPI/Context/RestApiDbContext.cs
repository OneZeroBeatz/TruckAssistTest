using Microsoft.EntityFrameworkCore;

namespace RestAPI.Context
{
    public class RestApiDbContext : DbContext
    {
        public RestApiDbContext(DbContextOptions options)
            : base(options)
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<Group> Groups { get; set; }
    }
}
