using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class AddLocalPassword_vmodel
    {
        [Required, Display(Name = "Contraseña local"), DataType(DataType.Password)]
        public string LocalPassword { get; set; }

        [Required, Display(Name = "Confirmar contraseña local"), DataType(DataType.Password)]
        [Compare("LocalPassword", ErrorMessage = "La nueva contraseña local y su confirmacion deben coincidir")]
        public string ConfirmPassword { get; set; }
    }
}