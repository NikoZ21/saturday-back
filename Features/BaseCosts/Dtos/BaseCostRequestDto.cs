using System.ComponentModel.DataAnnotations;

namespace Saturday_Back.Features.BaseCosts.Dtos
{
    public class BaseCostRequestDto
    {
        [Required]
        [StringLength(20)]
        public string StudyYear { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }
    }
}

