using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AptechProject3.DTOs.Report
{
    public class UpdateReportRequestDto
    {
        [Required]
        public int ReportId { get; set; }   
        public string ReportName { get; set; }
        public string Content { get; set; }
    }
}
