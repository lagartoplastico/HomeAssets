using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class ChangePassword_vmodel
    {
        [Required, Display(Name = "Contraseña Actual"), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required, Display(Name = "Contraseña Nueva"), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, Display(Name = "Confirmar Contraseña"), DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y su confirmacion no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}