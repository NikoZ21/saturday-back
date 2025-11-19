using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Saturday_Back.Dtos;
using Saturday_Back.Entities;
using Saturday_Back.Repositories;

namespace Saturday_Back.Services
{
    public class BaseCostService(ICachedRepository<BaseCost, BaseCostResponseDto> repository)
    {
        private readonly ICachedRepository<BaseCost, BaseCostResponseDto> _repository = repository;

        public Task<List<BaseCostResponseDto>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(BaseCost entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(BaseCost entity) => _repository.UpdateAsync(entity);
    }
}
