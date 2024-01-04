using AptechProject3.DTOs.ClientService;
using AptechProject3.DTOs.Service;
using AptechProject3.Models;
using AptechProject3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AptechProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientServiceController : ControllerBase
    {
        public readonly ILogger<ClientController> _logger;
        public readonly IClientServiceService _clientServiceService;
        public readonly IClientService _clientService;
        public readonly IServiceService _serviceService;
        public ClientServiceController(ILogger<ClientController> logger, IClientServiceService clientServiceService, IClientService clientService, IServiceService serviceService)
        {
            _logger = logger;
            _clientServiceService = clientServiceService;
            _clientService = clientService;
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> ClientServices()
        {
            try
            {
                var services = await _clientServiceService.GetAll();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientService(CreateClientServiceRequestDto request)
        {
            try
            {
                var client = await _clientService.GetById(request.ClientId);
                var service = await _serviceService.GetById(request.ClientId);

                if (service != null && client != null)
                {

                    var newClientService = new ClientService
                    {
                        Client = client,
                        Service = service,
                        Status = request.Status,
                    };

                    if (request.Status == "Valid")
                    {
                        request.StartDay = DateTime.Now.ToString();
                        int totalDay = service.TotalDay;
                        request.ExpiredDay = DateTime.Now.AddDays(totalDay).ToString();
                        newClientService.StartDay = request.StartDay;
                        newClientService.ExpiredDay = request.ExpiredDay;
                    }

                    return Ok(new { ClientService = newClientService, Message = "Create Succes!" });
                }
                else
                {
                    return BadRequest("Client or Service not found , please try again !");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateClientService(CreateClientServiceRequestDto request)
        {
            try
            {
                if (request.ClientServiceId == 0)
                {
                    return BadRequest("Invalid Service");
                }

                var client = await _clientService.GetById(request.ClientId);
                var service = await _serviceService.GetById(request.ClientId);

                if (service != null && client != null)
                {
                    var updateClientService = new ClientService
                    {
                        ClientServiceId = request.ClientServiceId,
                        Client = client,
                        Service = service,
                        Status = request.Status,
                        StartDay = request.StartDay,
                        ExpiredDay = request.ExpiredDay
                    };
                    return Ok(new { ClientService = updateClientService, Message = "Update Success!" });

                }
                else
                {
                    return BadRequest("Service or Client is null ");

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
                    var clientService = await _clientServiceService.GetById(id);
                    if (clientService != null)
                    {
                        await _clientServiceService.Delete(id);
                        return Ok();
                    }
                    return BadRequest("Client Service not found");
                }
                return BadRequest("Client Service not found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
