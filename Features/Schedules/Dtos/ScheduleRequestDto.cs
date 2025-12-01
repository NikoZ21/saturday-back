using System.ComponentModel.DataAnnotations;
using Saturday_Back.Common.Enums;

namespace Saturday_Back.Features.Schedules.Dtos
{
    public class ScheduleRequestDto
    {
        // Student information (will be created if doesn't exist)
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Identificator { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public PaymentTypeValue PaymentType { get; set; }

        [Required]
        public BenefitTypeValue BenefitType { get; set; }

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [Range(1, 30)]
        public int FirstSaturday { get; set; }

        [Required]
        [Range(1, 30)]
        public int LastSaturday { get; set; }

        [Required]
        [Range(10, 17)]
        public int FirstMonth { get; set; }

        [Required]
        [Range(10, 17)]
        public int LastMonth { get; set; }

        [Required]
        public string StudyYear { get; set; } = string.Empty;
    }
}

