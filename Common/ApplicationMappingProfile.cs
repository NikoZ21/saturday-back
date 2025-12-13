using AutoMapper;
using Saturday_Back.Features.AcademicYears;
using Saturday_Back.Features.AcademicYears.Dtos;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.BenefitTypes.Dtos;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.PaymentTypes.Dtos;
using Saturday_Back.Features.ScheduleEntries;
using Saturday_Back.Features.Schedules;
using Saturday_Back.Features.Schedules.Dtos;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.Students.Dtos;
using Saturday_Back.Features.Subjects;
using Saturday_Back.Features.Subjects.Dtos;

namespace Saturday_Back.Common
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            // PaymentType mappings
            CreateMap<PaymentTypeRequestDto, PaymentType>();
            CreateMap<PaymentType, PaymentTypeResponseDto>();

            // BenefitType mappings
            CreateMap<BenefitTypeRequestDto, BenefitType>();
            CreateMap<BenefitType, BenefitTypeResponseDto>();

            // Subject mappings
            CreateMap<SubjectRequestDto, Subject>();
            CreateMap<Subject, SubjectResponseDto>();

            // Student mappings
            CreateMap<StudentRequestDto, Student>();
            CreateMap<Student, StudentResponseDto>()
                .ForMember(dest => dest.AdmissionYearRange,
                          opt => opt.MapFrom(src => src.AdmissionYear != null ? src.AdmissionYear.YearRange : null));
            // Schedule mappings
            CreateMap<ScheduleRequestDto, Schedule>();
            CreateMap<Schedule, ScheduleResponseDto>()
                .ForMember(dest => dest.Subject,
                          opt => opt.MapFrom(src => src.Subject != null ? src.Subject.Name : "default subject"))
                .ForMember(dest => dest.PaymentType,
                          opt => opt.MapFrom(src => src.PaymentType != null ? src.PaymentType.Name : "default payment type"))
                .ForMember(dest => dest.BenefitType,
                          opt => opt.MapFrom(src => src.BenefitType != null ? src.BenefitType.Name : "default benefit type"));
            CreateMap<ScheduleEntry, ScheduleEntryResponseDto>();
            // AcademicYear mappings
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

