using HomeAssets.Models;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class CreateHomeService_vm
    {
        [Required, Display(Name = "Ubicación")]
        public Locations Location { get; set; }

        [Required, Display(Name = "Tipo de Servicio")]
        public ServiceTypes ServiceType { get; set; }

        [Required, Display(Name = "Institución")]
        public Institutions Institution { get; set; }

        [Required, Display(Name = "A nombre de")]
        public Members LeasedTo { get; set; }

        [Required, Display(Name = "Criterio de pago")]
        public PaymentCriterias PaymentCriteria { get; set; }

        [Required, Display(Name = "Código de pago")]
        public string PaymentId { get; set; }
    }
}