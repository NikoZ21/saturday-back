using System.ComponentModel.DataAnnotations;
using Saturday_Back.Common.Enums;
using Saturday_Back.Features.Schedules.Validations;

namespace Saturday_Back.Features.Schedules.Dtos
{
    public class ScheduleRequestDto
    {
        // Must have information to create a schedule
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Identificator { get; set; } = string.Empty;

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string StudyYear { get; set; } = string.Empty;


        // Optional information
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        public int PaymentType { get; set; } = 1;

        public int BenefitType { get; set; } = 1;

        [Range(1, 30)]
        public int FirstSaturday { get; set; } = 1;

        [Range(1, 30)]
        [EnsureAfter("FirstSaturday")]
        public int LastSaturday { get; set; } = 30;

        [Range(10, 17)]
        public int FirstMonth { get; set; } = 10;

        [Range(10, 17)]
        [EnsureAfter("FirstMonth")]
        public int LastMonth { get; set; } = 17;

    }
}

