using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<IdentityUser> IdentityUsers { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }


    }
}
