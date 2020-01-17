using HomeAssets.Models;
using HomeAssets.Models.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class EditAccount_vmodel
    {
        public EditAccount_vmodel()
        {
            Claims = new List<string>();
        }

        [Required, HiddenInput]
        public string Id { get; set; }

        [Required, Display(Name = "Nombre de usuario"), StringLength(10)]
        [RegularExpression("[a-z]{3,}",
               ErrorMessage = "Solo se permiten letras minusculas y el username debe tener al menos 3 letras")]
        public string Username { get; set; }

        [Required, Display(Name = "Correo Electrónico"), EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Formato invalido de correo electrónico")]
        [ValidEmailDomain]
        public string Email { get; set; }

        [Display(Name = "Genero")]
        public Genders Gender { get; set; } = Genders.Ninguno;

        public IList<string> Claims { get; set; }
    }
}