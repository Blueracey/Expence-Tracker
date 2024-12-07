using expenceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace expenceTracker.Data
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext>options): base(options) { }

        public DbSet<actualExpence> actualExpences { get; set; }

        public DbSet<expectedExpences> expectedExpence { get; set; } 


        public DbSet<monthlyExpence> monthlyExpence { get; set; }

        public DbSet<User> Users    { get; set; }



    }
}
