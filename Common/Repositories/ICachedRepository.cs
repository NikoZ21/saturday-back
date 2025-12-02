using System.Linq.Expressions;

namespace Saturday_Back.Common.Repositories
{
    public interface ICachedRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>?> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RefreshCacheAsync();
    }
}

