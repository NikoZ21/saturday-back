using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Entities;
using Saturday_Back.Services;

namespace Saturday_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly PaymentTypeService _paymentTypeService;
        public PaymentTypeController(PaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var paymentTypes = await _paymentTypeService.GetAllAsync();

            return Ok(paymentTypes);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] PaymentType payment)
        {
            await _paymentTypeService.UpdatePaymentTypeAsync(payment);
            return Ok("Payment type updated and cache refreshed");
        }
    }
}
