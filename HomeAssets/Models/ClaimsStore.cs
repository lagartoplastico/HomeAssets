using System.Collections.Generic;
using System.Security.Claims;

namespace HomeAssets.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            // Roles
            new Claim("Role","Administrador CON permisos de modificación"),
            new Claim("Role","Administrador SIN permisos de modificación"),
            new Claim("Role","Usuario CON permisos de modificación"),
            new Claim("Role","Usuario SIN permisos de modificación")
        };
    }
}