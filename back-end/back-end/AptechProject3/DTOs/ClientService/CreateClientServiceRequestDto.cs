using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.ClientService
{
    public class CreateClientServiceRequestDto
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientServiceId { get; set; } = 0;
        [Required]
        public int ClientId { get; set; } = 0;
        [Required]
        public int ServiceId { get; set; } = 0;
        [Required]
        public string Status { get; set; } = string.Empty;

        public string StartDay { get; set; } = string.Empty;

        public string ExpiredDay { get; set; } = string.Empty;
    }
}
