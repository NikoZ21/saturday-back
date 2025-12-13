using Saturday_Back.Features.PaymentTypes.Dtos;
using Saturday_Back.Features.BenefitTypes.Dtos;
using Saturday_Back.Features.Subjects.Dtos;

namespace Saturday_Back.Features.GoogleSheetFields.Dtos
{
    public class GoogleSheetFieldsResponse
    {
        public List<string> PaymentTypes { get; set; } = [];
        public List<string> BenefitTypes { get; set; } = [];
        public List<string> Subjects { get; set; } = [];

    }


}