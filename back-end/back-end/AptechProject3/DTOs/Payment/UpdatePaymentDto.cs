using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.Payment
{
    public class UpdatePaymentDto
    {
        [Required]
        public int PaymentId { get; set; }
        public string Status { get; set; }
    }
}
