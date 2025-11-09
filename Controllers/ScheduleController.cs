using Microsoft.AspNetCore.Mvc;

namespace Saturday_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GenerateSchedule()
        {
            return Ok("All the schedules");
        }
    }
}
