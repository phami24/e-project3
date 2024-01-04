using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.DTOs.Client;
using AptechProject3.Models;
using AptechProject3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AptechProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public readonly ILogger<ClientController> _logger;
        public readonly IClientService _clientService;
        public readonly IPaymentService _paymentService;
        public readonly IClientServiceService _clientServiceService;
        public ClientController(ILogger<ClientController> logger, IClientService clientService, IPaymentService paymentService, IClientServiceService clientServiceService)
        {
            _logger = logger;
            _clientService = clientService;
            _paymentService = paymentService;
            _clientServiceService = clientServiceService;
        }

        [HttpGet]
        public async Task<IActionResult> Clients()
        {
            try
            {
                var clients = await _clientService.GetAll();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateClient(UpdateClientRequestDto request)
        {
            try
            {
                if (request.ClientId != 0)
                {
                    var existingClient = await _clientService.GetById(request.ClientId);
                    if (existingClient == null)
                    {
                        return NotFound("Client not found");
                    }

                    var updateClient = new Client();
                    if (!string.IsNullOrEmpty(request.FirstName))
                    {
                        updateClient.FirstName = request.FirstName;
                    }
                    if (!string.IsNullOrEmpty(request.LastName))
                    {
                        updateClient.LastName = request.LastName;
                    }
                    if (!string.IsNullOrEmpty(request.Email))
                    {
                        updateClient.Email = request.Email;
                    }
                    if (request.PaymentIds != null)
                    {
                        List<Payment> payments = new List<Payment>();
                        foreach (int id in request.PaymentIds)
                        {
                            var payment = await _paymentService.GetById(id);
                            if (payment != null)
                            {
                                payments.Add(payment);
                            }
                        }
                        updateClient.Payments = payments;
                    }
                    if (request.ClientServiceIds != null)
                    {
                        List<ClientService> clientServices = new List<ClientService>();
                        foreach (int id in request.ClientServiceIds)
                        {
                            var clientService = await _clientServiceService.GetById(id);
                            if (clientService != null)
                            {
                                clientServices.Add(clientService);
                            }
                        }
                        updateClient.ClientServices = clientServices;
                    }
                    await _clientService.Update(updateClient);
                    return Ok(new { Client = updateClient, Message = "Update success!" });
                    ;
                }
                else
                {
                    return BadRequest("Invalid ClientId");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                await _clientService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateCheckoutRequest")]
        public async Task<ActionResult> CreateCheckoutRequest(CreateCheckoutRequestDto request)
        {
            try
            {
                if (request.ClientId == 0)
                {
                    return Unauthorized("User not authenticated");
                }
                var client = await _clientService.GetById(request.ClientId);
                if (client != null)
                {
                    var payment = new Payment
                    {
                        Amount = request.Amount,
                        PaymentDate = DateTime.Now.ToString(),
                        Client = client,
                    };
                    await _paymentService.Create(payment);
                    return (Ok(payment));
                }
                return BadRequest("Client not found");

            }
            catch (Exception ex)
            {
                return BadRequest("Error creating checkout session");
            }
        }
    }
}
