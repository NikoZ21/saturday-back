using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Features.AcademicYears.Dtos;

namespace Saturday_Back.Features.AcademicYears
{
    [Route("api/academic-years")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private readonly AcademicYearService _service;

        public AcademicYearController(AcademicYearService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AcademicYearResponseDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<AcademicYearResponseDto>> Create([FromBody] AcademicYearRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault();

                if (firstError != null)
                    return BadRequest(firstError.ErrorMessage);
            }

            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AcademicYearResponseDto>> Update(int id, [FromBody] AcademicYearRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, request);
            return Ok(updated);
        }
    }
}

