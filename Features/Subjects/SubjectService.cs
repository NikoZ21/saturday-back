using AutoMapper;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.Subjects.Dtos;

namespace Saturday_Back.Features.Subjects
{
    public class SubjectService
    {
        private readonly ICachedRepository<Subject> _repository;
        private readonly IMapper _mapper;

        public SubjectService(ICachedRepository<Subject> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SubjectResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<SubjectResponseDto>>(entities);
        }

        public async Task<SubjectResponseDto> CreateAsync(SubjectRequestDto request)
        {
            var entity = _mapper.Map<Subject>(request);
            await _repository.AddAsync(entity);
            return _mapper.Map<SubjectResponseDto>(entity);
        }

        public async Task<SubjectResponseDto> UpdateAsync(int id, SubjectRequestDto request)
        {
            var entity = _mapper.Map<Subject>(request);
            entity.Id = id;
            await _repository.UpdateAsync(entity);
            return _mapper.Map<SubjectResponseDto>(entity);
        }
    }
}

