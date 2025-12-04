using AutoMapper;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.AcademicYears.Dtos;

namespace Saturday_Back.Features.AcademicYears
{
    public class AcademicYearService
    {
        private readonly ICachedRepository<AcademicYear> _repository;
        private readonly IMapper _mapper;

        public AcademicYearService(ICachedRepository<AcademicYear> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AcademicYearResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<AcademicYearResponseDto>>(entities ?? []);
        }

        public async Task<AcademicYearResponseDto> CreateAsync(AcademicYearRequestDto request)
        {
            var entity = _mapper.Map<AcademicYear>(request);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<AcademicYearResponseDto>(created);
        }

        public async Task<AcademicYearResponseDto> UpdateAsync(int id, AcademicYearRequestDto request)
        {
            var entity = _mapper.Map<AcademicYear>(request);
            entity.Id = id;
            await _repository.UpdateAsync(entity);
            return _mapper.Map<AcademicYearResponseDto>(entity);
        }
    }
}

