namespace Saturday_Back.Features.AcademicYears.Dtos
{
    public class AcademicYearResponseDto
    {
        public int Id { get; set; }
        public string YearRange { get; set; } = string.Empty;
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public decimal Cost { get; set; }
    }
}

