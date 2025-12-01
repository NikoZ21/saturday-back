using System.ComponentModel.DataAnnotations;

namespace Saturday_Back.Features.Students.Dtos
{
    public class StudentRequestDto
    {
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
        public int AdmissionYearId { get; set; }
    }
}

