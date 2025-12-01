using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Features.BaseCosts.Dtos;

namespace Saturday_Back.Features.BaseCosts
{
    [Route("api/base-costs")]
    [ApiController]
    public class BaseCostController : ControllerBase
    {
        private readonly BaseCostService _service;

        public BaseCostController(BaseCostService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BaseCostResponseDto>>> GetAll()
        {
            var baseCosts = await _service.GetAllAsync();
            return Ok(baseCosts);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCostResponseDto>> Create([FromBody] BaseCostRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseCostResponseDto>> Update(int id, [FromBody] BaseCostRequestDto request)
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

