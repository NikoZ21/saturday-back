namespace Saturday_Back.Dtos
{
    public class ScheduleRequestDto
    {
        public int SubjectId { get; set; }
        public int PaymentTypeId { get; set; }
        public int BenefitTypeId { get; set; }
        public int BaseCostId { get; set; }
        public int FirstSaturday { get; set; }
        public int LastSaturday { get; set; }
        public int FirstMonth { get; set; }
        public int LastMonth { get; set; }
    }
}