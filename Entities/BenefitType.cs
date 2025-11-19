using Saturday_Back.Enums;

namespace Saturday_Back.Entities
{
    public class BenefitType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Discount { get; set; }
        public BenefitTypeValue Value { get; set; }
    }
}
