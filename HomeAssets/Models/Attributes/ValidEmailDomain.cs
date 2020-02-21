using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.Models.Attributes
{
    public class ValidEmailDomain : ValidationAttribute
    {
        private readonly List<string> validVendors;
        private readonly List<string> validExtensions;

        public ValidEmailDomain()
        {
            validVendors = new List<string>()
            {
                "gmail",
                "outlook",
                "hotmail",
                "yahoo",
                "protonmail",
                "icloud"
            };

            validExtensions = new List<string>()
            {
                "com",
                "es"
            };

            ErrorMessage = "Correos validos: Gmail, Outlook. Hotmail, Yahoo, iCloud y Protonmail";
        }

        public override bool IsValid(object value)
        {
            string[] valueDomain = value.ToString().Split('@')[1].ToLower().Split('.');

            if (validExtensions.Find(extension => extension == valueDomain[1]) != null &&
                validVendors.Find(vendor => vendor == valueDomain[0]) != null)
            {
                return true;
            }
            return false;
        }
    }
}