namespace AptechProject3.DTOs.Payment
{
    public class ProcessPaymentDto
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Token { get; set; }
    }
}
