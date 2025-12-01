using Microsoft.EntityFrameworkCore;
using Saturday_Back.Common.Database;

namespace Saturday_Back.Common.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly FssDbContext _dbContext;

        public Repository(FssDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> WhereAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return addedEntity.Entity;
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
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var idProperty = typeof(TEntity).GetProperty("Id");
            var id = (int)idProperty?.GetValue(entity)!;

            var tracked = await _dbContext.Set<TEntity>().FindAsync(id);
            if (tracked == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with Id {id} not found");

            _dbContext.Set<TEntity>().Remove(tracked);
            await _dbContext.SaveChangesAsync();
        }
    }
}

