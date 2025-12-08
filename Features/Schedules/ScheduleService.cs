using AutoMapper;
using Saturday_Back.Common.Database;
using Saturday_Back.Common.Exceptions;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Schedules.Dtos;
using Saturday_Back.Features.Schedules.Interfaces;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.Subjects;

namespace Saturday_Back.Features.Schedules
{
    public class ScheduleService
    {
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IScheduleFieldResolver _fieldResolver;
        private readonly IScheduleEntriesResolver _scheduleEntriesResolver;
        private readonly IMapper _mapper;
        private readonly ScheduleFactory _scheduleFactory;
        private readonly FssDbContext _dbContext;

        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(
            IRepository<Schedule> scheduleRepository,
            IRepository<Student> studentRepository,
            IScheduleFieldResolver fieldResolver,
            IScheduleEntriesResolver entriesResolver,
            FssDbContext dbContext,
            IMapper mapper,
            ILogger<ScheduleService> logger)
        {
            _scheduleRepository = scheduleRepository;
            _studentRepository = studentRepository;
            _fieldResolver = fieldResolver;
            _scheduleEntriesResolver = entriesResolver;
            _dbContext = dbContext;
            _mapper = mapper;
            _scheduleFactory = new ScheduleFactory();
            _logger = logger;
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
                var fields = new ScheduleFields
                {
                    Student = await _fieldResolver.ResolveStudentAsync(request),
                    Subject = await _fieldResolver.ResolveSubjectAsync(request),
                    BenefitType = await _fieldResolver.ResolveBenefitTypeAsync(request),
                    PaymentType = await _fieldResolver.ResolvePaymentTypeAsync(request),
                };
                _logger.LogInformation("Resolved fields: {@Fields}", fields);

                await ValidateNoDuplicateScheduleAsync(fields, request.StudyYear);
                _logger.LogInformation("Validated no duplicate schedule");

                var scheduleEntries = _scheduleEntriesResolver.ResolveEntriesAsync(fields, request);
                _logger.LogInformation("Resolved schedule entries: {@ScheduleEntries}", scheduleEntries);

                var schedule = _scheduleFactory.Create(request, fields, scheduleEntries);
                _logger.LogInformation("Created schedule}");

                await _scheduleRepository.AddAsync(schedule);
                _logger.LogInformation("Added schedule to database");

                await transaction.CommitAsync();

                return _mapper.Map<ScheduleResponseDto>(schedule);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new BusinessRuleException("Failed to create schedule");
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
                throw new BusinessRuleException(
                    $"Schedule already exists for student '{entities.Student.Identificator}' " +
                    $"in subject '{entities.Subject.Name}' " +
                    $"for year '{studyYear}'");
            }
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

