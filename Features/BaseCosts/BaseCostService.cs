using AutoMapper;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.BaseCosts.Dtos;

namespace Saturday_Back.Features.BaseCosts
{
    public class BaseCostService
    {
        private readonly ICachedRepository<BaseCost> _repository;
        private readonly IMapper _mapper;

        public BaseCostService(ICachedRepository<BaseCost> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BaseCostResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync(
                bc => bc.StudyYear!);
            return _mapper.Map<List<BaseCostResponseDto>>(entities);
        }

        public async Task<BaseCostResponseDto> CreateAsync(BaseCostRequestDto request)
        {
            var entity = _mapper.Map<BaseCost>(request);
            await _repository.AddAsync(entity);
            return _mapper.Map<BaseCostResponseDto>(entity);
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

