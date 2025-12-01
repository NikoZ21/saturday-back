using Saturday_Back.Common.Enums;

namespace Saturday_Back.Features.BenefitTypes.Dtos
{
    public class BenefitTypeResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Discount { get; set; }
        public BenefitTypeValue Value { get; set; }
    }
}

