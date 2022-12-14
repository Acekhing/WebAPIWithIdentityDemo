using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPIWithIdentityDemo.Configurations
{
    public class RoleSeedingConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                    Name = "Staff",
                    NormalizedName = "STAFF"
                }
            );
        }
    }
}
