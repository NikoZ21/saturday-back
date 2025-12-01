using AutoMapper;
using Saturday_Back.Features.Subjects.Dtos;

namespace Saturday_Back.Features.Subjects
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectRequestDto, Subject>();
            CreateMap<Subject, SubjectResponseDto>();
        }
    }
}

