using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.Models
{
    public class Employee : IdentityUser
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public Department Department { get; set; }
        
    }
}
