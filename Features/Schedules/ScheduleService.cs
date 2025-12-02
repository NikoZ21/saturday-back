using AutoMapper;
using Saturday_Back.Common.Database;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.BaseCosts;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Schedules.Dtos;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.StudyYears;
using Saturday_Back.Features.Subjects;

namespace Saturday_Back.Features.Schedules
{
    public class ScheduleService
    {
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly ScheduleLookupService _lookupService;
        private readonly ScheduleEntriesGenerator _entriesGenerator;
        private readonly FssDbContext _dbContext;
        private readonly IMapper _mapper;

        public ScheduleService(
            IRepository<Schedule> scheduleRepository,
            IRepository<Student> studentRepository,
            ScheduleLookupService lookupService,
            ScheduleEntriesGenerator entriesGenerator,
            FssDbContext dbContext,
            IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _studentRepository = studentRepository;
            _lookupService = lookupService;
            _entriesGenerator = entriesGenerator;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ScheduleResponseDto>> GetAllAsync()
        {
            var entities = await _scheduleRepository.GetAllAsync(
                s => s.Subject!,
                s => s.PaymentType!,
                s => s.BenefitType!,
                s => s.BaseCost!);
            return _mapper.Map<List<ScheduleResponseDto>>(entities);
        }

        public async Task<ScheduleResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _scheduleRepository.GetByIdAsync(
                id,
                s => s.Subject!,
                s => s.PaymentType!,
                s => s.BenefitType!,
                s => s.BaseCost!);
            return entity == null ? null : _mapper.Map<ScheduleResponseDto>(entity);
        }

        public async Task<ScheduleResponseDto> CreateScheduleAsync(ScheduleRequestDto request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var entities = await FetchRequiredEntitiesAsync(request);

                await ValidateNoDuplicateScheduleAsync(entities);

                var cost = CalculateScheduleCost(request, entities);

                var entries = _entriesGenerator.Generate(
                    cost,
                    entities.PaymentType.Value,
                    request.FirstMonth,
                    request.LastMonth,
                    entities.StudyYear
                    );

                var schedule = BuildScheduleEntity(request, entities, entries);
                await _scheduleRepository.AddAsync(schedule);

                await transaction.CommitAsync();

                return _mapper.Map<ScheduleResponseDto>(schedule);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        #region Validation

        private async Task ValidateNoDuplicateScheduleAsync(ScheduleEntities entities)
        {
            var exists = await _scheduleRepository.FirstOrDefaultAsync(s =>
                s.StudentId == entities.Student.Id &&
                s.SubjectId == entities.Subject.Id &&
                s.StudyYearId == entities.StudyYear.Id);

            if (exists != null)
            {
                throw new InvalidOperationException(
                    $"Schedule already exists for student '{entities.Student.Identificator}' " +
                    $"in subject '{entities.Subject.Name}' " +
                    $"for year '{entities.StudyYear.YearRange}'");
            }
        }

        #endregion

        #region Entity Fetching

        private async Task<ScheduleEntities> FetchRequiredEntitiesAsync(ScheduleRequestDto request)
        {
            // Fetch lookup data
            var currentStudyYear = await _lookupService.GetStudyYearByRangeAsync(request.StudyYear);
            var subject = await _lookupService.GetSubjectByNameAsync(request.Subject);
            var benefitType = await _lookupService.GetBenefitTypeAsync(request.BenefitType);
            var paymentType = await _lookupService.GetPaymentTypeAsync(request.PaymentType);

            // Get or create student
            var student = await GetOrCreateStudentAsync(request, currentStudyYear);

            // Get base cost for student's admission year
            var baseCost = await _lookupService.GetBaseCostByYearAsync(student.AdmissionYear!);

            return new ScheduleEntities
            {
                StudyYear = currentStudyYear,
                Subject = subject,
                BenefitType = benefitType,
                PaymentType = paymentType,
                Student = student,
                BaseCost = baseCost
            };
        }

        private async Task<Student> GetOrCreateStudentAsync(ScheduleRequestDto request, StudyYear admissionYear)
        {
            var student = await _studentRepository
                .FirstOrDefaultAsync(s => s.Identificator == request.Identificator);

            if (student == null)
            {
                student = new Student
                {
                    Identificator = request.Identificator,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    AdmissionYearId = admissionYear.Id,
                };

                return await _studentRepository.AddAsync(student);
            }

            return student;
        }

        #endregion

        #region Calculation
        private decimal CalculateScheduleCost(ScheduleRequestDto request, ScheduleEntities entities)
        {
            if (entities.BaseCost.Cost == 0)
            {
                throw new InvalidOperationException("Base cost is 0. Please ensure the base cost is set correctly.");
            }

            var saturdaysCount = request.LastSaturday - request.FirstSaturday + 1;
            var totalDiscount = entities.BenefitType.Discount + (entities.PaymentType.Discount ?? 0);
            var discountedCost = entities.BaseCost.Cost * (1 - totalDiscount / 100);

            return discountedCost * (saturdaysCount / 30);
        }

        #endregion

        #region Entity Building

        private Schedule BuildScheduleEntity(
            ScheduleRequestDto request,
            ScheduleEntities entities,
            List<ScheduleEntry> entries)
        {
            return new Schedule
            {
                StudyYearId = entities.StudyYear.Id,
                StudentId = entities.Student.Id,
                SubjectId = entities.Subject.Id,
                PaymentTypeId = entities.PaymentType.Id,
                BenefitTypeId = entities.BenefitType.Id,
                BaseCostId = entities.BaseCost.Id,
                FirstSaturday = request.FirstSaturday,
                LastSaturday = request.LastSaturday,
                FirstMonth = request.FirstMonth,
                LastMonth = request.LastMonth,
                ScheduleEntries = entries
            };
        }

        #endregion

        #region Helper Classes

        private class ScheduleEntities
        {
            public Student Student { get; set; } = null!;
            public Subject Subject { get; set; } = null!;
            public StudyYear StudyYear { get; set; } = null!;
            public BenefitType BenefitType { get; set; } = null!;
            public PaymentType PaymentType { get; set; } = null!;
            public BaseCost BaseCost { get; set; } = null!;
        }

        #endregion
    }
}

