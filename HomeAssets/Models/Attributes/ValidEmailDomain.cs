using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.Models.Attributes
{
    public class ValidEmailDomain : ValidationAttribute
    {
        private readonly List<string> validVendors;

        public ValidEmailDomain()
        {
            validVendors = new List<string>()
            {
                "gmail.com",
                "outlook.com",
                "hotmail.com",
                "mailinator.com",
                "mail.com"
            };

            ErrorMessage = "Dominios validos: ";

            foreach (string vendor in validVendors)
            {
                ErrorMessage += $"@{vendor} ";
            }
        }

        public override bool IsValid(object value)
        {
            string valueVendor = value.ToString().Split('@')[1].ToLower();

            if (validVendors.Find(vendor => vendor == valueVendor) != null)
            {
                return true;
            }
            else return false;
        }
    }
}