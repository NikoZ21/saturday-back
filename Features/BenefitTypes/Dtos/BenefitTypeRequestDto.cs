using Saturday_Back.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Saturday_Back.Features.BenefitTypes.Dtos
{
    public class BenefitTypeRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, 100)]
        public decimal Discount { get; set; }

        [Required]
        public BenefitTypeValue Value { get; set; }
    }
}

