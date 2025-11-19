using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Saturday_Back.Repositories
{
    public class CachedRepository<TEntity, TDto> : ICachedRepository<TEntity, TDto> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey;  
        private readonly IMapper _mapper;

        public CachedRepository(DbContext dbContext, IMemoryCache cache, string cacheKey, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cacheKey = cacheKey ?? throw new ArgumentNullException(nameof(cacheKey));            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<TDto>?> GetAllAsync()
        {
            return await _cache.GetOrCreateAsync(_cacheKey, async entry =>
            {
                Console.WriteLine("Cache miss - loading from database.");
                var entities = await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
                return _mapper.Map<List<TDto>>(entities);
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
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            await RefreshCacheAsync();
        }

        public async Task RefreshCacheAsync()
        {
            var entities = await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            var dtos = _mapper.Map<List<TDto>>(entities);
            _cache.Set(_cacheKey, dtos);
        }
    }
}