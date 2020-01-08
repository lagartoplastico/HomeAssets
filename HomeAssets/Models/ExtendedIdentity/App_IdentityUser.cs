using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAssets.Models.ExtendedIdentity
{
    public class App_IdentityUser : IdentityUser
    {
        [Required]
        public Genders Gender { get; set; }
    }
}
