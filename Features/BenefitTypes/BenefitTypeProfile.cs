using AutoMapper;
using Saturday_Back.Features.BenefitTypes.Dtos;

namespace Saturday_Back.Features.BenefitTypes
{
    public class BenefitTypeProfile : Profile
    {
        public BenefitTypeProfile()
        {
            CreateMap<BenefitTypeRequestDto, BenefitType>();
            CreateMap<BenefitType, BenefitTypeResponseDto>();
        }
    }
}

