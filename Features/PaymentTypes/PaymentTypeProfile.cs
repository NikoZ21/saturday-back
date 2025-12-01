using AutoMapper;
using Saturday_Back.Features.PaymentTypes.Dtos;

namespace Saturday_Back.Features.PaymentTypes
{
    public class PaymentTypeProfile : Profile
    {
        public PaymentTypeProfile()
        {
            CreateMap<PaymentTypeRequestDto, PaymentType>();
            CreateMap<PaymentType, PaymentTypeResponseDto>();
        }
    }
}

