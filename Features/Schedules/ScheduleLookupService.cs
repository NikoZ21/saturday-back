using Saturday_Back.Common.Enums;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.BaseCosts;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Subjects;
using Saturday_Back.Features.StudyYears;

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
        private readonly IRepository<StudyYear> _studyYearRepository;
        private readonly IRepository<BaseCost> _baseCostRepository;

        public ScheduleLookupService(
            IRepository<BenefitType> benefitTypeRepository,
            IRepository<PaymentType> paymentTypeRepository,
            IRepository<Subject> subjectRepository,
            IRepository<StudyYear> studyYearRepository,
            IRepository<BaseCost> baseCostRepository)
        {
            _benefitTypeRepository = benefitTypeRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _subjectRepository = subjectRepository;
            _studyYearRepository = studyYearRepository;
            _baseCostRepository = baseCostRepository;
        }

        /// <summary>
        /// Gets a study year by its range (e.g., "2024-2025")
        /// </summary>
        public async Task<StudyYear> GetStudyYearByRangeAsync(string yearRange)
        {
            var studyYear = await _studyYearRepository.FirstOrDefaultAsync(sy => sy.YearRange == yearRange);

            if (studyYear == null)
                throw new KeyNotFoundException($"Study year '{yearRange}' not found. Please ensure the study year exists in the system.");

            return studyYear;
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

        /// <summary>
        /// Gets the base cost for a specific study year
        /// </summary>
        public async Task<BaseCost> GetBaseCostByYearAsync(StudyYear studyYear)
        {
            var baseCost = await _baseCostRepository.FirstOrDefaultAsync(bc => bc.StudyYearId == studyYear.Id);

            if (baseCost == null)
                throw new KeyNotFoundException($"Base cost not found for study year '{studyYear.YearRange}'. Please configure the base cost for this year.");

            return baseCost;
        }
    }
}

