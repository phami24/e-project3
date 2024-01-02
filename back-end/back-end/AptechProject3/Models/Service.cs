using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptechProject3.Models
{
    public class Service
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ICollection<ServiceCharges> ServicesCharges { get; set; }
    }
}
