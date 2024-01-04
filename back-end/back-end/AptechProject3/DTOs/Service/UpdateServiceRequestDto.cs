using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.Service
{
    public class UpdateServiceRequestDto
    {
        [Required]
        public int ServiceId { get; set; } = 0;
        public string ServiceName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<int> ServicesChargeIds { get; set; } = new List<int>();
    }
}
