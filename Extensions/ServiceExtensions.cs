using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPIWithIdentityDemo.Data;
using WebAPIWithIdentityDemo.Models;

namespace WebAPIWithIdentityDemo.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                //options.UseInMemoryDatabase(DatabaseContext.DatabaseName);
                options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=penpab;Integrated Security=True");
                // Configure more options here...
            });
        }

        public static void AddIdentityCore(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var identityBuilder = services.AddIdentity<User, IdentityRole>(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    // Configure development identity options here...

                    // Password requirements
                    options.Password.RequiredLength = DatabaseContext.DevelopmentMinimumPasswordLength;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                }
                else
                {
                    // Configure production identity options here...

                    // User requirements
                    options.User.RequireUniqueEmail = true;

                    // Password requirements
                    options.Password.RequiredLength = DatabaseContext.ProductionMinimumPasswordLength;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                }
            }
            ).AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

            //identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole) ,services);
            //identityBuilder.AddEntityFrameworkStores<DatabaseContext>();
            //identityBuilder.AddDefaultTokenProviders();
        }
    }
}
