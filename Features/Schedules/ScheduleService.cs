using AutoMapper;
using Saturday_Back.Common.Database;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Schedules.Dtos;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.Subjects;

namespace Saturday_Back.Features.Schedules
{
    public class ScheduleService
    {
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly ScheduleLookupService _lookupService;
        private readonly ScheduleEntriesGenerator _scheduleEntriesGenerator;
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
            _scheduleEntriesGenerator = entriesGenerator;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ScheduleResponseDto>> GetAllAsync()
        {
            var entities = await _scheduleRepository.GetAllAsync(
                s => s.Subject!,
                s => s.PaymentType!,
                s => s.BenefitType!);
            return _mapper.Map<List<ScheduleResponseDto>>(entities);
        }

        public async Task<ScheduleResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _scheduleRepository.GetByIdAsync(
                id,
                s => s.Subject!,
                s => s.PaymentType!,
                s => s.BenefitType!);
            return entity == null ? null : _mapper.Map<ScheduleResponseDto>(entity);
        }

        public async Task<ScheduleResponseDto> CreateScheduleAsync(ScheduleRequestDto request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var fields = await GetEntityFieldsAsync(request);

                await ValidateNoDuplicateScheduleAsync(fields, request.StudyYear);

                var scheduleEntries = _scheduleEntriesGenerator.Generate(fields, request);

                var schedule = BuildScheduleEntity(request, fields, scheduleEntries);
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

        private async Task ValidateNoDuplicateScheduleAsync(ScheduleFields entities, string studyYear)
        {
            var exists = await _scheduleRepository.FirstOrDefaultAsync(s =>
                s.StudentId == entities.Student.Id &&
                s.SubjectId == entities.Subject.Id &&
                s.StudyYear == studyYear);

            if (exists != null)
            {
                throw new InvalidOperationException(
                    $"Schedule already exists for student '{entities.Student.Identificator}' " +
                    $"in subject '{entities.Subject.Name}' " +
                    $"for year '{studyYear}'");
            }
        }

        #endregion

        #region Entity Fetching

        private async Task<ScheduleFields> GetEntityFieldsAsync(ScheduleRequestDto request)
        {
            // Fetch lookup data
            var subject = await _lookupService.GetSubjectByNameAsync(request.Subject);
            var benefitType = await _lookupService.GetBenefitTypeAsync(request.BenefitType);
            var paymentType = await _lookupService.GetPaymentTypeAsync(request.PaymentType);

            // Get or create student
            var student = await GetOrCreateStudentAsync(request);

            return new ScheduleFields
            {
                StudyYear = request.StudyYear,
                Subject = subject,
                BenefitType = benefitType,
                PaymentType = paymentType,
                Student = student,
            };
        }

        private async Task<Student> GetOrCreateStudentAsync(ScheduleRequestDto request)
        {
            var student = await _studentRepository
                .FirstOrDefaultAsync(s => s.Identificator == request.Identificator, s => s.AdmissionYear!);

            if (student == null)
            {
                var currentAcademicYear = await _lookupService.GetAcademicYearByRangeAsync(request.StudyYear);
                student = new Student
                {
                    Identificator = request.Identificator,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    AdmissionYearId = currentAcademicYear.Id,
                };

                return await _studentRepository.AddAsync(student, s => s.AdmissionYear!);
            }

            return student;
        }

        #endregion    

        #region Entity Building

        private static Schedule BuildScheduleEntity(
            ScheduleRequestDto request,
            ScheduleFields entities,
            List<ScheduleEntry> entries)
        {
            return new Schedule
            {
                StudyYear = request.StudyYear,
                StudentId = entities.Student.Id,
                SubjectId = entities.Subject.Id,
                PaymentTypeId = entities.PaymentType.Id,
                BenefitTypeId = entities.BenefitType.Id,
                FirstSaturday = request.FirstSaturday,
                LastSaturday = request.LastSaturday,
                FirstMonth = request.FirstMonth,
                LastMonth = request.LastMonth,
                ScheduleEntries = entries
            };
        }

        #endregion

        #region Helper Classes

        public class ScheduleFields
        {
            public Student Student { get; set; } = null!;
            public Subject Subject { get; set; } = null!;
            public string StudyYear { get; set; } = null!;
            public BenefitType BenefitType { get; set; } = null!;
            public PaymentType PaymentType { get; set; } = null!;
        }

        #endregion       
    }
}

