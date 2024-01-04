using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.Client
{
    public class CreateCheckoutRequestDto
    {
        [Required]
        public decimal Amount { get; set; } = 0;
        [Required]
        public string PaymentDate { get; set; } = string.Empty;
        [Required]
        public int ClientId { get; set; } = 0;
    }
}
