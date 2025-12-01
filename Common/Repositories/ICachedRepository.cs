namespace Saturday_Back.Common.Repositories
{
    public interface ICachedRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>?> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RefreshCacheAsync();
    }
}

