using System.ComponentModel.DataAnnotations;

namespace Saturday_Back.Features.StudyYears.Dtos
{
    public class StudyYearRequestDto
    {
        [Required]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "YearRange must be in format YYYY-YYYY (e.g., 2015-2016)")]
        [StringLength(9, MinimumLength = 9)]
        public string YearRange { get; set; } = string.Empty;
    }
}

