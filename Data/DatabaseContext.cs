using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPIWithIdentityDemo.Configurations;
using WebAPIWithIdentityDemo.Models;

namespace WebAPIWithIdentityDemo.Data
{
    public class DatabaseContext: IdentityDbContext<User>
    {
        public const string InMemoryDatabaseName = "PenpabCompanyDB";
        public const int DevelopmentMinimumPasswordLength = 4;
        public const int ProductionMinimumPasswordLength = 8;

        public DatabaseContext(DbContextOptions options): base(options)
        {
        }

        DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleSeedingConfiguration());
        }
    }
}
