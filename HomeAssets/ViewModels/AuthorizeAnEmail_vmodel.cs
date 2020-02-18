using HomeAssets.Models.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class AuthorizeAnEmail_vmodel
    {
        [Required, Display(Name = "Correo Electrónico"), EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Formato invalido de correo electrónico")]
        [Remote("IsEmailAlreadyAuthorized", "Administration")]
        [ValidEmailDomain]
        public string Email { get; set; }
    }
}