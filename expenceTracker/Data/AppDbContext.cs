using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<ActualExpense> ActualExpenses { get; set; }

        public DbSet<ExpenseMonth> ExpenseMonths { get; set; }

        public DbSet<ExpenseRecurringAndVariable> ExpenseRecurringAndVariables { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

    }
}
