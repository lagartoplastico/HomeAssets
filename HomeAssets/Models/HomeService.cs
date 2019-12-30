using System.ComponentModel.DataAnnotations;

namespace HomeAssets.Models
{
    public class HomeService
    {
        public int Id { get; set; }

        [Required]
        public Locations Location { get; set; }

        [Required]
        public ServiceTypes ServiceType { get; set; }

        [Required]
        public Institutions Institution { get; set; }

        [Required]
        public Members LeasedTo { get; set; }

        [Required]
        public PaymentCriterias PaymentCriteria { get; set; }

        [Required]
        public string PaymentId { get; set; }
    }
}