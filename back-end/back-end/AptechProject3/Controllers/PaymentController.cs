using AptechProject3.Configuration;
using AptechProject3.DTOs.Payment;
using AptechProject3.DTOs.Service;
using AptechProject3.Models;
using AptechProject3.Services;
using AptechProject3.Services.ServicesImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AptechProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        public readonly ILogger<ClientController> _logger;
        public readonly IPaymentService _paymentService;
        public readonly StripeSettings _stripeSettings;

        public PaymentController(ILogger<ClientController> logger, IPaymentService paymentService, StripeSettings stripeSettings)
        {
            _logger = logger;
            _paymentService = paymentService;
            _stripeSettings = stripeSettings;
        }
        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentDto paymentDto)
        {
            try
            {

                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;


                var options = new ChargeCreateOptions
                {
                    Amount = (long)(paymentDto.Amount * 100), // Stripe requires amount in cents
                    Currency = "usd", // Change to your currency
                    Source = paymentDto.Token, // Token obtained from Stripe.js
                    Description = "Payment for Aptech Project 3",
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);


                if (charge.Status == "succeeded")
                {
                    var updatePayment = new Payment
                    {
                        PaymentId = paymentDto.PaymentId,
                        Status = charge.Status,
                    };
                    await _paymentService.Update(updatePayment);
                    return Ok("Payment successful");
                }
                else
                {
                    var updatePayment = new Payment
                    {
                        PaymentId = paymentDto.PaymentId,
                        Status = "failed",
                    };
                    await _paymentService.Update(updatePayment);
                    return BadRequest("Payment failed");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateService(UpdatePaymentDto request)
        {
            try
            {
                if (request.PaymentId != 0)
                {
                    var existingPayment = await _paymentService.GetById(request.PaymentId);
                    if (existingPayment == null)
                    {
                        return NotFound("Service not found");
                    }

                    var updatePayment = new Payment()
                    {
                        PaymentId = request.PaymentId,
                        Status = request.Status
                    };
                    return Ok(new { Payment = updatePayment, Message = "Update success!" });
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
                    var service = await _paymentService.GetById(id);
                    if (service != null)
                    {
                        await _paymentService.Delete(id);
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
