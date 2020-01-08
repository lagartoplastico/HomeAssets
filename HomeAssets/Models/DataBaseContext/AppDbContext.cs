using HomeAssets.Models.ExtendedIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeAssets.Models.DataBaseContext
{
    public class AppDbContext : IdentityDbContext<App_IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<HomeService> HomeServices { get; set; }
    }
}