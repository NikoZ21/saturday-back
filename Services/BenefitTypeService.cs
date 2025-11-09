using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Saturday_Back.Entities;

namespace Saturday_Back.Services
{
    public class BenefitTypeService
    {
        private readonly FssDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "Benefits";

        public BenefitTypeService(FssDbContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<List<BenefitType>> GetAllAsync()
        {
            if (!_cache.TryGetValue(CacheKey, out List<BenefitType>? benefitTypes))
            {
                Console.WriteLine("Loading benefit types from database... and caching...");
                benefitTypes = await _dbContext.BenefitTypes.AsNoTracking().ToListAsync();
                _cache.Set(CacheKey, benefitTypes);
            }

            Console.WriteLine("Returning benefit types from cache...");
            return benefitTypes;
        }

        public async Task UpdateBenefitTypeAsync(BenefitType updatedBenefitType)
        {
            var existing = await _dbContext.BenefitTypes.FindAsync(updatedBenefitType.Id);

            if (existing != null)
            {
                existing.Name = updatedBenefitType.Name;
                existing.Discount = updatedBenefitType.Discount;
                existing.Value = updatedBenefitType.Value;

                await _dbContext.SaveChangesAsync();

                //refreshing the cache
                var paymentTypes = await _dbContext.PaymentTypes.AsNoTracking().ToListAsync();
                _cache.Set(CacheKey, paymentTypes);
            }
        }
    }
}
