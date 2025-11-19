using Saturday_Back.Dtos;
using Saturday_Back.Entities;
using Saturday_Back.Repositories;


namespace Saturday_Back.Services
{
    public class SubjectsService(ICachedRepository<Subject, SubjectResponseDto> repository)
    {
        private readonly ICachedRepository<Subject, SubjectResponseDto> _repository = repository;

        public Task<List<SubjectResponseDto>?> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(Subject entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(Subject entity) => _repository.UpdateAsync(entity);

    }
}
