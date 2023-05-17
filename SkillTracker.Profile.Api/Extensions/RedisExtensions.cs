using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillTracker.Profile.Data.DbContext;
using SkillTracker.Search.Cache;
using SkillTracker.Search.Cache.Interfaces;
using StackExchange.Redis;

namespace SkillTracker.Profile.Api.Extensions
{
    public static class RedisExtensions
    {
        public static void AddRedisCache(this WebApplicationBuilder builder, ILogger<Program> logger)
        {
            var services = builder.Services;
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var connectionString = configuration["CacheServiceConnectionString"];

            logger.LogInformation($"Redis Connectionstring: {connectionString}");
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
         
            });
            
            services.AddTransient<ICacheRepository, CacheRepository>();
        }

       
    }
}