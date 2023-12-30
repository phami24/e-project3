using System.ComponentModel.DataAnnotations;

namespace AptechProject3.Models
{
    public class ClientService
    {
        [Required]
        public int ClientServiceId { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Service Service { get; set; }
    }
}
