using AutoMapper;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.Students.Dtos;

namespace Saturday_Back.Features.Students
{
    public class StudentService
    {
        private readonly IRepository<Student> _repository;
        private readonly IMapper _mapper;

        public StudentService(IRepository<Student> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<StudentResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<StudentResponseDto>>(entities);
        }

        public async Task<StudentResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<StudentResponseDto>(entity);
        }

        public async Task<StudentResponseDto> CreateAsync(StudentRequestDto request)
        {
            var entity = _mapper.Map<Student>(request);
            await _repository.AddAsync(entity);
            return _mapper.Map<StudentResponseDto>(entity);
        }

        public async Task<StudentResponseDto> UpdateAsync(int id, StudentRequestDto request)
        {
            var entity = _mapper.Map<Student>(request);
            entity.Id = id;
            await _repository.UpdateAsync(entity);
            return _mapper.Map<StudentResponseDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Student with Id {id} not found");

            await _repository.DeleteAsync(entity);
        }
    }
}

