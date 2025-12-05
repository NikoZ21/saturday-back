using Saturday_Back.Common.Enums;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.AcademicYears;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Subjects;


namespace Saturday_Back.Features.Schedules
{
    /// <summary>
    /// Service responsible for looking up reference data needed for schedule creation.
    /// Encapsulates all repository queries for lookup entities (subjects, payment types, etc.)
    /// </summary>
    public class ScheduleLookupService
    {
        private readonly IRepository<BenefitType> _benefitTypeRepository;
        private readonly IRepository<PaymentType> _paymentTypeRepository;
        private readonly IRepository<Subject> _subjectRepository;
        private readonly IRepository<AcademicYear> _academicYearRepository;

        public ScheduleLookupService(
            IRepository<BenefitType> benefitTypeRepository,
            IRepository<PaymentType> paymentTypeRepository,
            IRepository<Subject> subjectRepository,
            IRepository<AcademicYear> academicYearRepository
        )
        {
            _benefitTypeRepository = benefitTypeRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _subjectRepository = subjectRepository;
            _academicYearRepository = academicYearRepository;
        }

        /// <summary>
        /// Gets an academic year by its range (e.g., "2024-2025")
        /// </summary>
        public async Task<AcademicYear> GetAcademicYearByRangeAsync(string yearRange)
        {
            var yearRangeValue = YearRangeValue.Parse(yearRange);

            var academicYear = await _academicYearRepository.FirstOrDefaultAsync(sy => sy.Range == yearRangeValue);

            if (academicYear == null)
                throw new KeyNotFoundException($"Academic year '{yearRange}' not found. Please ensure the academic year exists in the system.");

            return academicYear;
        }

        /// <summary>
        /// Gets a subject by its name
        /// </summary>
        public async Task<Subject> GetSubjectByNameAsync(string subjectName)
        {
            var subject = await _subjectRepository.FirstOrDefaultAsync(s => s.Name == subjectName);

            if (subject == null)
                throw new KeyNotFoundException($"Subject '{subjectName}' not found. Please ensure the subject exists in the system.");

            return subject;
        }

        /// <summary>
        /// Gets a benefit type by its enum value
        /// </summary>
        public async Task<BenefitType> GetBenefitTypeAsync(BenefitTypeValue benefitTypeValue)
        {
            var benefitType = await _benefitTypeRepository.FirstOrDefaultAsync(bt => bt.Value == benefitTypeValue);

            if (benefitType == null)
                throw new KeyNotFoundException($"Benefit type '{benefitTypeValue}' not found. Please ensure the benefit type exists in the system.");

            return benefitType;
        }

        /// <summary>
        /// Gets a payment type by its enum value
        /// </summary>
        public async Task<PaymentType> GetPaymentTypeAsync(PaymentTypeValue paymentTypeValue)
        {
            var paymentType = await _paymentTypeRepository.FirstOrDefaultAsync(pt => pt.Value == paymentTypeValue);

            if (paymentType == null)
                throw new KeyNotFoundException($"Payment type '{paymentTypeValue}' not found. Please ensure the payment type exists in the system.");

            return paymentType;
        }
    }
}

