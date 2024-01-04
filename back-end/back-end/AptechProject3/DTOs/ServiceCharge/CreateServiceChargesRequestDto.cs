using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.ServiceCharge
{
    public class CreateServiceChargesRequestDto
    {
        [Required]
        public string ServiceChargesName { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; } = 0;
        [Required]
        public string ServiceChargesDescription { get; set; } = string.Empty;
    }
}
