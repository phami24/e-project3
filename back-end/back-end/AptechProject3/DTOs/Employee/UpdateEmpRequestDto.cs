using AptechProject3.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.Employee
{
    public class UpdateEmpRequestDto
    {
        [Required]
        public int EmployeeId { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        public int DepartmentId { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
    }
}
