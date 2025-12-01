using Microsoft.Extensions.Caching.Memory;
using Saturday_Back.Common.Database;

namespace Saturday_Back.Common.Repositories
{
    public static class CachedRepositoryExtensions
    {
        public static IServiceCollection AddCachedRepoWithService<TEntity, TService>(
         this IServiceCollection services,
         string cacheKey)         
         where TEntity : class
         where TService : class
        {
            // Register the cached repository
            services.AddScoped<ICachedRepository<TEntity>>(sp =>
            {
                var db = sp.GetRequiredService<FssDbContext>();
                var cache = sp.GetRequiredService<IMemoryCache>();

                return new CachedRepository<TEntity>(db, cache, cacheKey);
            });

            // Register the service that uses the repository
            services.AddScoped<TService>();

            return services;
        }
    }
}
