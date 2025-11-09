using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Saturday_Back.Entities;

namespace Saturday_Back.Services
{
    public class PaymentTypeService
    {
        private readonly FssDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "PaymentTypes";

        public PaymentTypeService(FssDbContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<List<PaymentType>> GetAllAsync()
        {
            if (!_cache.TryGetValue(CacheKey, out List<PaymentType>? paymentTypes))
            {
                Console.WriteLine("Loading payment types from database... and caching...");
                paymentTypes = await _dbContext.PaymentTypes.AsNoTracking().ToListAsync();
                _cache.Set(CacheKey, paymentTypes);
            }

            Console.WriteLine("Returning payment types from cache...");
            return paymentTypes;
        }

        public async Task UpdatePaymentTypeAsync(PaymentType updatedPayment)
        {
            var existing = await _dbContext.PaymentTypes.FindAsync(updatedPayment.Id);

            if (existing != null)
            {
                existing.Name = updatedPayment.Name;
                existing.Discount = updatedPayment.Discount;
                existing.Value = updatedPayment.Value;

                await _dbContext.SaveChangesAsync();

                //refresh cache
                var paymentTypes = await _dbContext.PaymentTypes.AsNoTracking().ToListAsync();
                _cache.Set(CacheKey, paymentTypes);
            }
        }
    }
}
