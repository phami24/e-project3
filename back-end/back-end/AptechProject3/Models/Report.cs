using System.ComponentModel.DataAnnotations;

namespace AptechProject3.Models
{
    public class Report
    {
        [Required]
        public int ReportId { get; set; }
        [Required]
        public string ReportName { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
