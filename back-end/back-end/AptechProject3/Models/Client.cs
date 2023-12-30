using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.Models
{
    public class Client : IdentityUser
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public ICollection<ClientService> ClientServices { get; set; }
        [Required]
        public ICollection<Payment> Payments { get; set; }
    }
}
