using System.ComponentModel.DataAnnotations;

namespace AptechProject3.Models
{
    public class ServiceCharges
    {
        [Required]
        public int ServiceChargesId { get; set; }
        [Required]
        public  string ServiceChargesName { get; set; }
        [Required]
        public double Price{ get; set; }
        [Required]
        public string ServiceChargesDescription { get; set; }

    }

}
