using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Features.Schedules.Dtos;

namespace Saturday_Back.Features.Schedules
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _service;

        public ScheduleController(ScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScheduleResponseDto>>> GetAll()
        {
            var schedules = await _service.GetAllAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleResponseDto>> GetById(int id)
        {
            var schedule = await _service.GetByIdAsync(id);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleResponseDto>> Create([FromBody] ScheduleRequestDto request)
        {
            Console.WriteLine("enterign here");
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault();

                Console.WriteLine("nterign here");

                if (firstError != null)
                    return BadRequest(firstError.ErrorMessage);
            }

            // var result = await _service.CreateScheduleAsync(request);
            return Ok("created successfully");
        }
    }
}

