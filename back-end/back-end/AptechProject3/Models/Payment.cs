using System.ComponentModel.DataAnnotations;

namespace AptechProject3.Models
{
    public class Payment
    {
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public Client Client { get; set; }
    }
}
