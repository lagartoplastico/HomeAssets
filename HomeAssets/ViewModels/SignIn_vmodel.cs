using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class SignIn_vmodel
    {
        [Required, DataType(DataType.Text), Display(Name = "Usuario o Correo Electrónico")]
        [StringLength(64, ErrorMessage = "Limite de caracteres sobrepasado")]
        public string UserOrEmail { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordarme?")]
        public bool PersistentCookies { get; set; }
    }
}