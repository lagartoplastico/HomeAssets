using HomeAssets.Models;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class CreateHomeService_vm
    {
        [Required, Display(Name = "Tipo de Servicio")]
        public ServiceTypes ServiceType { get; set; }

        [Required, Display(Name = "Institución")]
        public string Institution { get; set; }

        [Required, Display(Name = "A nombre de")]
        public Members LeasedTo { get; set; }

        [Required, Display(Name = "Criterio de pago")]
        public string PaymentCriteria { get; set; }

        [Required, Display(Name = "Código de pago")]
        public string PaymentId { get; set; }
    }
}