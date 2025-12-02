using AutoMapper;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.BaseCosts.Dtos;
using Saturday_Back.Features.StudyYears;

namespace Saturday_Back.Features.BaseCosts
{
    public class BaseCostService
    {
        private readonly ICachedRepository<BaseCost> _repository;
        private readonly ICachedRepository<StudyYear> _studyYearRepository;
        private readonly IMapper _mapper;

        public BaseCostService(ICachedRepository<BaseCost> repository, ICachedRepository<StudyYear> studyYearRepository, IMapper mapper)
        {
            _repository = repository;
            _studyYearRepository = studyYearRepository;
            _mapper = mapper;
        }

        public async Task<List<BaseCostResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync(
                bc => bc.StudyYear!);
            return _mapper.Map<List<BaseCostResponseDto>>(entities);
        }

        public async Task<List<BaseCostResponseDto>> CreateAsync(BaseCostRequestDto request)
        {

            var entity = _mapper.Map<BaseCost>(request);
            var studyYear = await _studyYearRepository.AddAsync(_mapper.Map<StudyYear>(request.StudyYear));
            entity.StudyYearId = studyYear.Id;
            await _repository.AddAsync(entity);
            return await GetAllAsync();
        }

        public async Task<BaseCostResponseDto> UpdateAsync(int id, BaseCostRequestDto request)
        {
            var entity = _mapper.Map<BaseCost>(request);
            entity.Id = id;
            await _repository.UpdateAsync(entity);
            return _mapper.Map<BaseCostResponseDto>(entity);
        }
    }
}

