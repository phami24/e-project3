using AptechProject3.Models;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.Client
{
    public class UpdateClientRequestDto
    {
        [Required]
        public int ClientId { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<int> ClientServiceIds { get; set; } = new List<int>();
        public List<int> PaymentIds { get; set; } = new List<int>();
    }
}
