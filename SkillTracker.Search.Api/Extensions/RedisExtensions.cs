using SkillTracker.Search.Cache;
using SkillTracker.Search.Cache.Interfaces;
using StackExchange.Redis;

namespace SkillTracker.Search.Api.Extensions
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
                options.ConfigurationOptions.ConnectTimeout = 10000;

            });
            
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));
            services.AddTransient<ICacheRepository, CacheRepository>();
        }

       
    }
}