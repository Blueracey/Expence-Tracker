using expenceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace expenceTracker.Data
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext>options): base(options) { }

        public DbSet<actualExpence> actualExpences { get; set; }

        public DbSet<expenceMonth> expenceMonths { get; set; }

        public DbSet<expenceRecurringAndVariable>expenceRecurringAndVariables { get; set; }

        public DbSet<User> Users    { get; set; }

        public DbSet<userProfile> userProfiles { get; set; }

    }
}
