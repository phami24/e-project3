using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.Department
{
    public class UpdateDepartmentRequestDto
    {
        [Required]
        public int DepartmentId { get; set; } = 0;
        public string DepartmentName { get; set; } = string.Empty;

        public List<int> EmployeeIds { get; set; } = new List<int>();
    }

}
