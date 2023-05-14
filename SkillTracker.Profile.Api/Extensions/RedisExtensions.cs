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
        public static void AddRedisCache(this  WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var hostName = Environment.GetEnvironmentVariable("CacheServiceHost");
            var connectionString = configuration["CacheServiceConnectionString"].Replace("<host>", hostName);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
            });
            
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));
            services.AddTransient<ICacheRepository, CacheRepository>();
        }

       
    }
}