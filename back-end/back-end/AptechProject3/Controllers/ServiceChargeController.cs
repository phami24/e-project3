using AptechProject3.DTOs.Service;
using AptechProject3.DTOs.ServiceCharge;
using AptechProject3.Models;
using AptechProject3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AptechProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceChargeController : ControllerBase
    {
        public readonly ILogger<ClientController> _logger;
        public readonly IServiceService _serviceService;
        public readonly IServiceChargesService _chargesService;
        public ServiceChargeController(ILogger<ClientController> logger, IServiceService serviceService, IServiceChargesService chargesService)
        {
            _logger = logger;
            _serviceService = serviceService;
            _chargesService = chargesService;
        }

        [HttpGet]
        public async Task<IActionResult> ServiceCharges()
        {
            try
            {
                var serviceCharges = await _chargesService.GetAll();
                return Ok(serviceCharges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceChargesRequestDto request)
        {
            try
            {
                var exitingServiceCharge = await _chargesService.GetByName(request.ServiceChargesName);
                if (exitingServiceCharge == null & !string.IsNullOrEmpty(request.ServiceChargesName))
                {
                    var newServiceCharge = new ServiceCharges
                    {
                        ServiceChargesName = request.ServiceChargesName,
                        Price = request.Price,
                        ServiceChargesDescription = request.ServiceChargesDescription
                    };

                    await _chargesService.Create(newServiceCharge);
                    return Ok(new { ServiceCharges = newServiceCharge, Message = "Create success!" });
                }
                else if (exitingServiceCharge != null)
                {
                    return BadRequest("Service is ex");
                }
                return BadRequest("Create fail! Please try again . ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateService(UpdateServiceChargesRequestDto request)
        {
            try
            {
                if (request.ServiceChargesId != 0)
                {
                    var existingService = await _chargesService.GetById(request.ServiceChargesId);
                    if (existingService == null)
                    {
                        return NotFound("Service not found");
                    }

                    var updateServiceCharge = new ServiceCharges
                    {
                        ServiceChargesId = request.ServiceChargesId
                    };
                    if (request.ServiceChargesName != null)
                    {
                        updateServiceCharge.ServiceChargesName = request.ServiceChargesName;
                    }
                    if (request.ServiceChargesDescription != null)
                    {
                        updateServiceCharge.ServiceChargesDescription = request.ServiceChargesDescription;
                    }

                    return Ok(new { Service = updateServiceCharge, Message = "Update success!" });
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
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                if (id != 0)
                {
                    var serviceCharge = await _chargesService.GetById(id);
                    if (serviceCharge != null)
                    {
                        await _chargesService.Delete(id);
                        return Ok();
                    }
                    return BadRequest("Service Charge not found");
                }
                return BadRequest("Service Charge not found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
