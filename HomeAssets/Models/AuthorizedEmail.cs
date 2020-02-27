using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAssets.Models
{
    public class AuthorizedEmail
    {
        public int id { get; set; }
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime DateOfCreation { get; set; }
    }
}
