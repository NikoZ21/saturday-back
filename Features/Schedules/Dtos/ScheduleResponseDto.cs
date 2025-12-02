namespace Saturday_Back.Features.Schedules.Dtos
{
    public class ScheduleResponseDto
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? PaymentType { get; set; }
        public string? BenefitType { get; set; }
        public decimal? BaseCost { get; set; }
        public int FirstSaturday { get; set; }
        public int LastSaturday { get; set; }
        public int FirstMonth { get; set; }
        public int LastMonth { get; set; }
        public List<ScheduleEntryDto> ScheduleEntries { get; set; } = [];
    }

    public class ScheduleEntryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
    }
}

