using HomeAssets.Models.ExtendedIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeAssets.Models.DataBaseContext
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            var passHash = new PasswordHasher<App_IdentityUser>();

            var user = new App_IdentityUser()
            {
                Id = "suseradm-su01-9283-7465-k01joannes07",
                UserName = "superuser",
                NormalizedUserName = "SUPERUSER",
                Email = "su@jdevops.xyz",
                NormalizedEmail = "SU@jdevops.xyz",
                EmailConfirmed = true,
                LockoutEnabled = true
            };

            user.PasswordHash = passHash.HashPassword(user, "$$$uperuser2020");

            builder.Entity<App_IdentityUser>().HasData(user);

            builder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>()
                {
                    Id = 999999999,
                    UserId = "suseradm-su01-9283-7465-k01joannes07",
                    ClaimType = "Role",
                    ClaimValue = "admin1"
                });
        }
    }
}