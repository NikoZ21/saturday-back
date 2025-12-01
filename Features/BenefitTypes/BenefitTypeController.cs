using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Features.BenefitTypes.Dtos;

namespace Saturday_Back.Features.BenefitTypes
{
    [Route("api/benefit-types")]
    [ApiController]
    public class BenefitTypeController : ControllerBase
    {
        private readonly BenefitTypeService _service;

        public BenefitTypeController(BenefitTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BenefitTypeResponseDto>>> GetAll()
        {
            var benefitTypes = await _service.GetAllAsync();
            return Ok(benefitTypes);
        }

        [HttpPost]
        public async Task<ActionResult<BenefitTypeResponseDto>> Create([FromBody] BenefitTypeRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BenefitTypeResponseDto>> Update(int id, [FromBody] BenefitTypeRequestDto request)
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

