using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Features.PaymentTypes.Dtos;

namespace Saturday_Back.Features.PaymentTypes
{
    [Route("api/payment-types")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly PaymentTypeService _service;

        public PaymentTypeController(PaymentTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentTypeResponseDto>>> GetAll()
        {
            var paymentTypes = await _service.GetAllAsync();
            return Ok(paymentTypes);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentTypeResponseDto>> Create([FromBody] PaymentTypeRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentTypeResponseDto>> Update(int id, [FromBody] PaymentTypeRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateAsync(id, request);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

