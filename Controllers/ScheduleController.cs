using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Entities;
using Saturday_Back.Enums;

namespace Saturday_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(ScheduleService scheduleService) : ControllerBase
    {

        private readonly ScheduleService _scheduleService = scheduleService;

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("All the schedules");
        }

        [HttpPost]
        public IActionResult CreateSchedule()
        {
            var benefitType = new BenefitType { Id = 1, Name = "Benefit Type", Discount = 10, Value = BenefitTypeValue.NONE };
            var paymentType = new PaymentType { Id = 1, Name = "Payment Type", Discount = 10, Value = PaymentTypeValue.ONETIME };
            // var baseCost = new BaseCost { Id = 1, StudyYear = "2025", Cost = 100 };
            var saturdaysCount = 30;
            var firstMonth = 10;
            var lastMonth = 17;

            var schedule = _scheduleService.BuildPaymentSchedule(benefitType, paymentType, 390, saturdaysCount, firstMonth, lastMonth);
            // Console.WriteLine(schedule);
            return Ok(schedule);
        }
    }
}
