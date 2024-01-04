using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.ServiceCharge
{
    public class UpdateServiceChargesRequestDto
    {
        [Required]
        public int ServiceChargesId { get; set; }
        public string ServiceChargesName { get; set; }
        public double Price { get; set; }
        public string ServiceChargesDescription { get; set; }
    }
}
