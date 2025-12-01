using AutoMapper;
using Saturday_Back.Features.StudyYears.Dtos;

namespace Saturday_Back.Features.StudyYears
{
    public class StudyYearProfile : Profile
    {
        public StudyYearProfile()
        {
            CreateMap<StudyYearRequestDto, StudyYear>();
            CreateMap<StudyYear, StudyYearResponseDto>();
        }
    }
}

