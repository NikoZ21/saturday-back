using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Subjects;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.ScheduleEntries;

namespace Saturday_Back.Features.Schedules
{
    public class Schedule
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        public int BenefitTypeId { get; set; }
        public BenefitType? BenefitType { get; set; }

        public int FirstSaturday { get; set; }
        public int LastSaturday { get; set; }
        public int FirstMonth { get; set; }
        public int LastMonth { get; set; }
        public string? StudyYear { get; set; }

        // Navigation property - one Schedule has many ScheduleEntries
        public List<ScheduleEntry> ScheduleEntries { get; set; } = [];
    }
}

