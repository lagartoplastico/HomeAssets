using System.Collections.Generic;
using System.Security.Claims;

namespace HomeAssets.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            // Roles
            new Claim("Role","admin1"),
            new Claim("Role","admin2"),
            new Claim("Role","user1"),
            new Claim("Role","user2")
        };
    }
}