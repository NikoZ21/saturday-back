using System.ComponentModel.DataAnnotations;

namespace Saturday_Back.Features.AcademicYears.Dtos
{
    public class AcademicYearRequestDto
    {
        [Required]
        public string YearRange { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }
    }
}

