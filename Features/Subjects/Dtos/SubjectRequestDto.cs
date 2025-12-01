using System.ComponentModel.DataAnnotations;

namespace Saturday_Back.Features.Subjects.Dtos
{
    public class SubjectRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}

