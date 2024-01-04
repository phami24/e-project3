using AptechProject3.DTOs.Report;
using AptechProject3.DTOs.Service;
using AptechProject3.Models;
using AptechProject3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AptechProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        public readonly ILogger<ClientController> _logger;
        public readonly IReportService _reportService;
        public ReportController(ILogger<ClientController> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Reports()
        {
            try
            {
                var services = await _reportService.GetAll();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(Report report)
        {
            try
            {

                var newReport = new Report
                {
                    ReportName = report.ReportName,
                    Content = report.Content
                };

                await _reportService.Create(newReport);
                return Ok(new { Report = newReport, Message = "Create success!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateDepartment(UpdateReportRequestDto request)
        {
            try
            {
                if (request.ReportId != 0)
                {
                    var existingReport = await _reportService.GetById(request.ReportId);
                    if (existingReport == null)
                    {
                        return NotFound("Report not found");
                    }

                    var updateReport = new Report
                    {
                        ReportName = request.ReportName,
                        Content = request.Content
                    };

                    return Ok(new { Report = updateReport, Message = "Update success!" });
                }
                else
                {
                    return BadRequest("Invalid Service");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                if (id != 0)
                {
                    var report = await _reportService.GetById(id);
                    if (report != null)
                    {
                        await _reportService.Delete(id);
                        return Ok();
                    }
                    return BadRequest("Service not found");
                }
                return BadRequest("Service not found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
