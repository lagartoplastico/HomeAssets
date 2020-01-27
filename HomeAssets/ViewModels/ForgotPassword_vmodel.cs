using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class ForgotPassword_vmodel
    {
        [Required, EmailAddress, Display(Name ="Correo electrónico")]
        public string Email { get; set; }
    }
}