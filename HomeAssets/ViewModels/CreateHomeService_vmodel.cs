using HomeAssets.Models;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class CreateHomeService_vmodel
    {
        [Required, Display(Name = "Ubicación")]
        public Locations Location { get; set; }

        [Required, Display(Name = "Tipo de Servicio")]
        public ServiceTypes ServiceType { get; set; }

        [Required, Display(Name = "Institución")]
        public Institutions Institution { get; set; }

        [Required, Display(Name = "A nombre de")]
        public Members LeasedTo { get; set; }

        [Required, Display(Name = "Criterio")]
        public PaymentCriterias PaymentCriteria { get; set; }

        [Required, Display(Name = "Valor de criterio")]
        [MaxLength(24, ErrorMessage = "El codigo debe tener como maximo 24 caracteres")]
        public string PaymentId { get; set; }
    }
}