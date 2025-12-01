using AutoMapper;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.StudyYears.Dtos;

namespace Saturday_Back.Features.StudyYears
{
    public class StudyYearService
    {
        private readonly ICachedRepository<StudyYear> _repository;
        private readonly IMapper _mapper;

        public StudyYearService(ICachedRepository<StudyYear> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<StudyYearResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<StudyYearResponseDto>>(entities);
        }

        public async Task<StudyYearResponseDto> CreateAsync(StudyYearRequestDto request)
        {
            var entity = _mapper.Map<StudyYear>(request);
            await _repository.AddAsync(entity);
            return _mapper.Map<StudyYearResponseDto>(entity);
        }

        public async Task<StudyYearResponseDto> UpdateAsync(int id, StudyYearRequestDto request)
        {
            var entity = _mapper.Map<StudyYear>(request);
            entity.Id = id;
            await _repository.UpdateAsync(entity);
            return _mapper.Map<StudyYearResponseDto>(entity);
        }
    }
}

