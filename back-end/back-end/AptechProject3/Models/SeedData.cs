using AptechProject3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AuthDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AuthDbContext>>()))
            {
                // Look for any movies.
                if (context.IdentityRoles.Any())
                {
                    return;  
                }
                context.IdentityRoles.AddRange
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                    new IdentityRole() { Name = "Employee", ConcurrencyStamp = "2", NormalizedName = "Employee" },
                    new IdentityRole() { Name = "User", ConcurrencyStamp = "3", NormalizedName = "User" }
                );
                context.SaveChanges();
            }
        }
    }
}
