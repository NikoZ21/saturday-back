using AutoMapper;
using Saturday_Back.Features.Students.Dtos;

namespace Saturday_Back.Features.Students
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentRequestDto, Student>();
            CreateMap<Student, StudentResponseDto>()
                .ForMember(dest => dest.AdmissionYearRange, 
                          opt => opt.MapFrom(src => src.AdmissionYear != null ? src.AdmissionYear.YearRange : null));
        }
    }
}

