using System.ComponentModel.DataAnnotations;

namespace AptechProject3.Models
{
    public class Service
    {
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ICollection<ServiceCharges> ServicesCharges { get; set; }
    }
}
