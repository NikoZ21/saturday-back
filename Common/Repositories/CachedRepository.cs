using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Saturday_Back.Common.Database;

namespace Saturday_Back.Common.Repositories
{
    public class CachedRepository<TEntity> : ICachedRepository<TEntity> where TEntity : class
    {
        private readonly FssDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey;

        public CachedRepository(FssDbContext dbContext, IMemoryCache cache, string cacheKey)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cacheKey = cacheKey ?? throw new ArgumentNullException(nameof(cacheKey));
        }

        public async Task<List<TEntity>?> GetAllAsync()
        {
            return await _cache.GetOrCreateAsync(_cacheKey, async entry =>
            {
                Console.WriteLine($"Cache miss for {_cacheKey} - loading from database.");
                return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            });
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            await RefreshCacheAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var idProperty = typeof(TEntity).GetProperty("Id");
            var id = (int)idProperty?.GetValue(entity)!;

            var tracked = await _dbContext.Set<TEntity>().FindAsync(id);
            if (tracked == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with Id {id} not found");

            _dbContext.Entry(tracked).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            await RefreshCacheAsync();
        }

        public async Task RefreshCacheAsync()
        {
            var entities = await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            _cache.Set(_cacheKey, entities);
        }
    }
}

