using Saturday_Back.Common.Exceptions;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.AcademicYears;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Schedules.Dtos;
using Saturday_Back.Features.Schedules.Interfaces;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.Subjects;


namespace Saturday_Back.Features.Schedules
{
    /// <summary>
    /// Service responsible for looking up reference data needed for schedule creation.
    /// Encapsulates all repository queries for lookup entities (subjects, payment types, etc.)
    /// </summary>
    public class ScheduleFieldResolver : IScheduleFieldResolver
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Subject> _subjectRepository;
        private readonly ICachedRepository<BenefitType> _benefitTypeRepository;
        private readonly ICachedRepository<PaymentType> _paymentTypeRepository;
        private readonly ICachedRepository<AcademicYear> _academicYearRepository;

        public ScheduleFieldResolver(
            IRepository<Student> studentRepository,
            IRepository<Subject> subjectRepository,
            ICachedRepository<BenefitType> benefitTypeRepository,
            ICachedRepository<PaymentType> paymentTypeRepository,
            ICachedRepository<AcademicYear> academicYearRepository
        )
        {
            _studentRepository = studentRepository;
            _benefitTypeRepository = benefitTypeRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _subjectRepository = subjectRepository;
            _academicYearRepository = academicYearRepository;
        }

        public async Task<Student> ResolveStudentAsync(ScheduleRequestDto request)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(s => s.Identificator == request.Identificator, s => s.AdmissionYear!);
            if (student == null)
            {
                var yearRangeValue = YearRangeValue.Parse(request.StudyYear);

                var academicYear = await _academicYearRepository.FirstOrDefaultAsync(sy => sy.Range == yearRangeValue) ??
                throw new BusinessRuleException(
                $"Academic year '{request.StudyYear}' not found. Please ensure the academic year exists in the system.",
                $"სასწავლო წელი '{request.StudyYear}' ვერ მოიძებნა ბაზაში. დარწმუნდით, რომ სასწავლო წელი სისტემაში არსებობს");

                student = new Student { Identificator = request.Identificator, FirstName = request.FirstName, LastName = request.LastName, AdmissionYearId = academicYear.Id };
                return await _studentRepository.AddAsync(student, s => s.AdmissionYear!);
            }
            return student;
        }
        public async Task<Subject> ResolveSubjectAsync(ScheduleRequestDto request)
        {
            var subject = await _subjectRepository.FirstOrDefaultAsync(s => s.Id == request.Subject) ??
            throw new BusinessRuleException(
                $"Subject '{request.Subject}' not found. Please ensure the subject exists in the system.",
                $"საგანი '{request.Subject}' ვერ მოიძებნა ბაზაში. დარწმუნდით, რომ საგანი სისტემაში არსებობს");
            return subject;
        }
        public async Task<PaymentType> ResolvePaymentTypeAsync(ScheduleRequestDto request)
        {
            var paymentType = await _paymentTypeRepository.FirstOrDefaultAsync(pt => pt.Id == request.PaymentType) ??
            throw new BusinessRuleException(
                $"Payment type '{request.PaymentType}' not found. Please ensure the payment type exists in the system.",
                $"გადახდის ტიპი '{request.PaymentType}' ვერ მოიძებნა ბაზაში. დარწმუნდით, რომ გადახდის ტიპი სისტემაში არსებობს");
            return paymentType;
        }
        public async Task<BenefitType> ResolveBenefitTypeAsync(ScheduleRequestDto request)
        {
            var benefitType = await _benefitTypeRepository.FirstOrDefaultAsync(bt => bt.Id == request.BenefitType) ??
            throw new BusinessRuleException(
                $"Benefit type '{request.BenefitType}' not found. Please ensure the benefit type exists in the system.",
                $"შეღავათის ტიპი '{request.BenefitType}' ვერ მოიძებნა ბაზაში. დარწმუნდით, რომ შეღავათის ტიპი სისტემაში არსებობს");
            return benefitType;
        }
    }
}

