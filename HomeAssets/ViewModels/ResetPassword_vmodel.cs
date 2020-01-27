using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class ResetPassword_vmodel
    {
        [Required, EmailAddress, Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirmar Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas deben ser iguales")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}