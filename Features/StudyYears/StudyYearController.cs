using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Features.StudyYears.Dtos;

namespace Saturday_Back.Features.StudyYears
{
    [Route("api/study-years")]
    [ApiController]
    public class StudyYearController : ControllerBase
    {
        private readonly StudyYearService _service;

        public StudyYearController(StudyYearService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudyYearResponseDto>>> GetAll()
        {
            var studyYears = await _service.GetAllAsync();
            return Ok(studyYears);
        }

        [HttpPost]
        public async Task<ActionResult<StudyYearResponseDto>> Create([FromBody] StudyYearRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudyYearResponseDto>> Update(int id, [FromBody] StudyYearRequestDto request)
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

