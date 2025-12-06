using Saturday_Back.Features.Schedules.Dtos;
using static Saturday_Back.Features.Schedules.ScheduleService;

namespace Saturday_Back.Features.Schedules
{
    public class ScheduleFactory
    {
        public Schedule Create(
            ScheduleRequestDto request,
            ScheduleFields fields,
            List<ScheduleEntry> entries)
        {
            return new Schedule
            {
                StudyYear = request.StudyYear,
                StudentId = fields.Student.Id,
                SubjectId = fields.Subject.Id,
                PaymentTypeId = fields.PaymentType.Id,
                BenefitTypeId = fields.BenefitType.Id,
                FirstSaturday = request.FirstSaturday,
                LastSaturday = request.LastSaturday,
                FirstMonth = request.FirstMonth,
                LastMonth = request.LastMonth,
                ScheduleEntries = entries
            };
        }
    }
}