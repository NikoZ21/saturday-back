using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Saturday_Back.Dtos;
using Saturday_Back.Entities;
using Saturday_Back.Repositories;

namespace Saturday_Back.Services
{
    public class BenefitTypeService(ICachedRepository<BenefitType, BenefitTypeResponseDto> repository)
    {
        private readonly ICachedRepository<BenefitType, BenefitTypeResponseDto> _repository = repository;        

        public Task<List<BenefitTypeResponseDto>> GetAllAsync() => _repository.GetAllAsync();

        public Task UpdatAsync(BenefitType entity) => _repository.UpdateAsync(entity);
    }
}
