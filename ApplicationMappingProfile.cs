using AutoMapper;
using Saturday_Back.Dtos;
using Saturday_Back.Entities;

namespace Saturday_Back
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<PaymentType, PaymentTypeResponseDto>();
            CreateMap<BenefitType, BenefitTypeResponseDto>();
            CreateMap<BaseCost, BaseCostResponseDto>();
            CreateMap<Subject, SubjectResponseDto>();
        }
    }
}
