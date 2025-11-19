using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Saturday_Back.Repositories
{
    public static class CachedRepositoryExtensions
    {
        public static IServiceCollection AddCachedRepoWithService<TEntity, TDto, TService>(
         this IServiceCollection services,
         string cacheKey)         
         where TEntity : class
         where TService : class
        {
            // Register the generic cached repository
            services.AddScoped<ICachedRepository<TEntity, TDto>>(sp =>
            {
                var db = sp.GetRequiredService<FssDbContext>();
                var cache = sp.GetRequiredService<IMemoryCache>();
                var mapper = sp.GetRequiredService<AutoMapper.IMapper>();

                return new CachedRepository<TEntity, TDto>(db, cache, cacheKey, mapper);
            });

            // Register the service that uses the repository
            services.AddScoped<TService>();

            return services;
        }
    }
}
