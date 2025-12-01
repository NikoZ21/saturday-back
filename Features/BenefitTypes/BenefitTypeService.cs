using AutoMapper;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.BenefitTypes.Dtos;

namespace Saturday_Back.Features.BenefitTypes
{
    public class BenefitTypeService
    {
        private readonly ICachedRepository<BenefitType> _repository;
        private readonly IMapper _mapper;

        public BenefitTypeService(ICachedRepository<BenefitType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BenefitTypeResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<BenefitTypeResponseDto>>(entities);
        }

        public async Task<BenefitTypeResponseDto> CreateAsync(BenefitTypeRequestDto request)
        {
            var entity = _mapper.Map<BenefitType>(request);
            await _repository.AddAsync(entity);
            return _mapper.Map<BenefitTypeResponseDto>(entity);
        }

        public async Task<BenefitTypeResponseDto> UpdateAsync(int id, BenefitTypeRequestDto request)
        {
            var entity = _mapper.Map<BenefitType>(request);
            entity.Id = id;
            await _repository.UpdateAsync(entity);
            return _mapper.Map<BenefitTypeResponseDto>(entity);
        }
    }
}

