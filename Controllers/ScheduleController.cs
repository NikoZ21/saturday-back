using Microsoft.AspNetCore.Mvc;

namespace Saturday_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private readonly ScheduleService _scheduleService;

        public ScheduleController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("All the schedules");
        }

        [HttpPost]
        public IActionResult CreateSchedule()
        {            
            return Ok("Created Schedule");
        }
    }
}
