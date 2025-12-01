using AutoMapper;
using Saturday_Back.Features.BaseCosts.Dtos;

namespace Saturday_Back.Features.BaseCosts
{
    public class BaseCostProfile : Profile
    {
        public BaseCostProfile()
        {
            CreateMap<BaseCostRequestDto, BaseCost>();
            CreateMap<BaseCost, BaseCostResponseDto>();
        }
    }
}

