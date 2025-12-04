using AutoMapper;
using Saturday_Back.Features.AcademicYears.Dtos;

namespace Saturday_Back.Features.AcademicYears
{
    public class AcademicYearProfile : Profile
    {
        public AcademicYearProfile()
        {
            CreateMap<AcademicYearRequestDto, AcademicYear>()
                .ForMember(dest => dest.Range,
                    opt => opt.MapFrom(src => YearRangeValue.Parse(src.YearRange)));

            CreateMap<AcademicYear, AcademicYearResponseDto>()
                .ForMember(dest => dest.YearRange,
                    opt => opt.MapFrom(src => src.YearRange))
                .ForMember(dest => dest.StartYear,
                    opt => opt.MapFrom(src => src.StartYear))
                .ForMember(dest => dest.EndYear,
                    opt => opt.MapFrom(src => src.EndYear));
        }
    }
}

