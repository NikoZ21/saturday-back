using Saturday_Back.Features.BaseCosts;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Subjects;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.StudyYears;

namespace Saturday_Back.Features.Schedules
{
    public class Schedule
    {
        public int Id { get; set; }
        public int StudyYearId { get; set; }
        public StudyYear? StudyYear { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        public int BenefitTypeId { get; set; }
        public BenefitType? BenefitType { get; set; }

        public int BaseCostId { get; set; }
        public BaseCost? BaseCost { get; set; }

        public int FirstSaturday { get; set; }
        public int LastSaturday { get; set; }
        public int FirstMonth { get; set; }
        public int LastMonth { get; set; }

        public List<ScheduleEntry> ScheduleEntries { get; set; } = [];
    }

    public class ScheduleEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
    }
}

