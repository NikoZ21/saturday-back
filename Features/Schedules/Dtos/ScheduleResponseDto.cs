namespace Saturday_Back.Features.Schedules.Dtos
{
    public class ScheduleResponseDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int PaymentTypeId { get; set; }
        public string? PaymentTypeName { get; set; }
        public int BenefitTypeId { get; set; }
        public string? BenefitTypeName { get; set; }
        public int BaseCostId { get; set; }
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

