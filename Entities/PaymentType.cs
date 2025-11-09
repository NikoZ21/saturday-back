using Saturday_Back.Enums;
using System.ComponentModel.DataAnnotations;

namespace Saturday_Back.Entities
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Discount { get; set; }
        public PaymentTypeValue Value { get; set; }
    }
}
