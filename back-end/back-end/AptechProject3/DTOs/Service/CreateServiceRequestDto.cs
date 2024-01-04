using AptechProject3.Models;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.Service
{
    public class CreateServiceRequestDto
    {
        [Required]
        public string ServiceName { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public List<int> ServicesChargeIds { get; set; } = new List<int>();
    }
}
