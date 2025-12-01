using Saturday_Back.Common.Enums;

namespace Saturday_Back.Features.PaymentTypes.Dtos
{
    public class PaymentTypeResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Discount { get; set; }
        public PaymentTypeValue Value { get; set; }
    }
}

