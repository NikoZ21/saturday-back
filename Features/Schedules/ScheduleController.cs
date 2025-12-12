using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Common.Exceptions;
using Saturday_Back.Features.Schedules.Dtos;

namespace Saturday_Back.Features.Schedules
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _service;
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(ScheduleService service, ILogger<ScheduleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Schedule>> GetAll()
        {
            var schedules = await _service.GetAllAsync();
            return Ok(schedules[0]);
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
                    throw new ValidationException(firstError.ErrorMessage);
            }

            _logger.LogInformation("Creating schedule for request: {@Request}", request);

            var result = await _service.CreateScheduleAsync(request);
            return Ok(result);
        }
    }
}

