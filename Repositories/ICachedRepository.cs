namespace Saturday_Back.Repositories
{
    public interface ICachedRepository<TEntity, TDto> where TEntity : class
    {
        Task<List<TDto>?> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RefreshCacheAsync();
    }
}
