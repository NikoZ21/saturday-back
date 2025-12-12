using Saturday_Back.Features.Schedules;

namespace Saturday_Back.Features.ScheduleEntries
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        // Foreign key to Schedule
        public int ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
    }
}