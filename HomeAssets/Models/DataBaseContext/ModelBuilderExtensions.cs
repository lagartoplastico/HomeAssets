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
                Id = "susususu-su01-9283-7465-001abcdetrn5",
                UserName = "superuser",
                NormalizedUserName = "SUPERUSER",
                Email = "superuser@superuser.local",
                NormalizedEmail = "SUPERUSER@SUPERUSER.LOCAL",
                EmailConfirmed = true
            };

            user.PasswordHash = passHash.HashPassword(user, "$$$uperuser");

            builder.Entity<App_IdentityUser>().HasData(user);

            builder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>()
                {
                    Id = 999999999,
                    UserId = "susususu-su01-9283-7465-001abcdetrn5",
                    ClaimType = "Role",
                    ClaimValue = "Administrador CON permisos de modificación"
                });
        }
    }
}