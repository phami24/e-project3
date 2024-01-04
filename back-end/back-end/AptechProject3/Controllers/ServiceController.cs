using AptechProject3.DTOs.Department;
using AptechProject3.DTOs.Service;
using AptechProject3.Models;
using AptechProject3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AptechProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        public readonly ILogger<ClientController> _logger;
        public readonly IServiceService _serviceService;
        public readonly IServiceChargesService _chargesService;
        public ServiceController(ILogger<ClientController> logger, IServiceService serviceService, IServiceChargesService chargesService)
        {
            _logger = logger;
            _serviceService = serviceService;
            _chargesService = chargesService;
        }

        [HttpGet]
        public async Task<IActionResult> Services()
        {
            try
            {
                var services = await _serviceService.GetAll();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceRequestDto request)
        {
            try
            {
                var exitingService = await _serviceService.GetByName(request.ServiceName);
                if (exitingService == null & !string.IsNullOrEmpty(request.ServiceName))
                {
                    var newService = new Service
                    {
                        ServiceName = request.ServiceName,
                        Description = request.Description,
                    };
                    if (request.ServicesChargeIds != null)
                    {
                        foreach (int id in request.ServicesChargeIds)
                        {
                            var chargeService = await _chargesService.GetById(id);
                            if (chargeService != null)
                            {
                                newService.ServicesCharges.Add(chargeService);
                            }
                        }
                    }
                    await _serviceService.Create(newService);
                    return Ok(new { Service = newService, Message = "Create success!" });
                }
                else if (exitingService != null)
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
        public async Task<IActionResult> UpdateService(UpdateServiceRequestDto request)
        {
            try
            {
                if (request.ServiceId != 0)
                {
                    var existingService = await _serviceService.GetById(request.ServiceId);
                    if (existingService == null)
                    {
                        return NotFound("Service not found");
                    }

                    var updateService = new Service()
                    {
                        ServiceId = request.ServiceId
                    };
                    if (request.ServiceName != null)
                    {
                        updateService.ServiceName = request.ServiceName;
                    }
                    if (request.Description != null)
                    {
                        updateService.Description = request.Description;
                    }
                    if (request.ServicesChargeIds != null)
                    {
                        foreach (int id in request.ServicesChargeIds)
                        {
                            var chargeService = await _chargesService.GetById(id);
                            if (chargeService != null)
                            {
                                updateService.ServicesCharges.Add(chargeService);
                            }
                        }
                    }
                    return Ok(new { Service = updateService, Message = "Update success!" });
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
                    var service = await _serviceService.GetById(id);
                    if (service != null)
                    {
                        await _serviceService.Delete(id);
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

