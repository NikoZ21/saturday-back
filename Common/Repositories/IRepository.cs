using System.Linq.Expressions;

namespace Saturday_Back.Common.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> AddAsync(TEntity entity, params Expression<Func<TEntity, object>>[] includes);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}

