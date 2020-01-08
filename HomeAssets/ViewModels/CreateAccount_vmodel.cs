using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class CreateAccount_vmodel
    {
        [Required, Display(Name = "Nombre de usuario"), StringLength(10)]
        [RegularExpression("[a-z]{3,}",
            ErrorMessage = "Solo se permiten letras minusculas y el username debe tener al menos 3 letras")]
        [Remote("IsUsernameInUse", "Account")]
        public string Username { get; set; }

        [Required, Display(Name = "Correo Electrónico"), EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Formato invalido de correo electrónico")]
        [Remote("IsEmailInUse", "Account")]
        public string Email { get; set; }

        [Required, Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password), Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}