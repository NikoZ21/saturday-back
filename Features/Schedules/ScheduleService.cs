using AutoMapper;
using Saturday_Back.Common.Database;
using Saturday_Back.Common.Exceptions;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.ScheduleEntries;
using Saturday_Back.Features.Schedules.Dtos;
using Saturday_Back.Features.Schedules.Interfaces;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.Subjects;

namespace Saturday_Back.Features.Schedules
{
    public class ScheduleService(IRepository<Schedule> scheduleRepository, ScheduleEntriesService scheduleEntryService, IScheduleFieldResolver fieldResolver, FssDbContext dbContext, IMapper mapper, ILogger<ScheduleService> logger)
    {
        private readonly IRepository<Schedule> _scheduleRepository = scheduleRepository;
        private readonly ScheduleEntriesService _scheduleEntriesService = scheduleEntryService;
        private readonly IScheduleFieldResolver _fieldResolver = fieldResolver;
        private readonly IMapper _mapper = mapper;
        private readonly ScheduleFactory _scheduleFactory = new ScheduleFactory();
        private readonly FssDbContext _dbContext = dbContext;
        private readonly ILogger<ScheduleService> _logger = logger;

        public async Task<List<ScheduleResponseDto>> GetAllAsync()
        {
            var entities = await _scheduleRepository.GetAllAsync(
                s => s.Student!,
                s => s.Subject!,
                s => s.PaymentType!,
                s => s.BenefitType!);
            foreach (var entity in entities)
            {
                entity.ScheduleEntries = await _scheduleEntriesService.GetAllByScheduleIdAsync(entity.Id);
            }
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
                _logger.LogInformation("Resolved fields: Student: {Student}, Subject: {Subject}, BenefitType: {BenefitType}, PaymentType: {PaymentType}", fields.Student.Identificator, fields.Subject.Name, fields.BenefitType.Value, fields.PaymentType.Value);

                await ValidateNoDuplicateScheduleAsync(fields, request.StudyYear);
                _logger.LogInformation("Validated no duplicate schedule");

                var schedule = _scheduleFactory.Create(request, fields);
                _logger.LogInformation("Created schedule}");

                var addedSchedule = await _scheduleRepository.AddAsync(schedule, s => s.ScheduleEntries);
                _logger.LogInformation("Added schedule to database");

                await _scheduleEntriesService.CreateAsync(addedSchedule.Id, fields.PaymentType, fields.BenefitType, fields.Student.AdmissionYear!, request.FirstSaturday, request.LastSaturday, request.FirstMonth, request.LastMonth);

                await transaction.CommitAsync();

                return _mapper.Map<ScheduleResponseDto>(addedSchedule);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new BusinessRuleException("Schedule Transaction failed: " + ex.Message);
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

