using Saturday_Back.Features.Schedules.Dtos;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.Subjects;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using static Saturday_Back.Features.Schedules.ScheduleService;

namespace Saturday_Back.Features.Schedules.Interfaces
{
    public interface IScheduleFieldResolver
    {
        Task<Student> ResolveStudentAsync(ScheduleRequestDto request);
        Task<Subject> ResolveSubjectAsync(ScheduleRequestDto request);
        Task<PaymentType> ResolvePaymentTypeAsync(ScheduleRequestDto request);
        Task<BenefitType> ResolveBenefitTypeAsync(ScheduleRequestDto request);
    }
}