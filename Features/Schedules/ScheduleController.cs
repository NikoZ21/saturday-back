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

        [HttpPost]
        public async Task<ActionResult<ScheduleResponseDto>> Create([FromBody] ScheduleRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault();

                if (firstError != null)
                    return BadRequest(firstError.ErrorMessage);
            }

            var result = await _service.CreateScheduleAsync(request);
            return Ok("created successfully");
        }
    }
}

