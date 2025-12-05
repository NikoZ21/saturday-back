using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            return await ApplyIncludes(includes)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            return await ApplyIncludes(includes).FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await ApplyIncludes(includes).FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await ApplyIncludes(includes).Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity, params Expression<Func<TEntity, object>>[] includes)
        {
            var addedEntity = await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // If includes are provided, reload the entity with navigation properties
            if (includes != null && includes.Length > 0)
            {
                var idProperty = typeof(TEntity).GetProperty("Id");
                var id = (int)idProperty?.GetValue(addedEntity.Entity)!;
                var reloaded = await ApplyIncludes(includes)
                    .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
                return reloaded ?? addedEntity.Entity;
            }

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
        private IQueryable<TEntity> ApplyIncludes(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}

