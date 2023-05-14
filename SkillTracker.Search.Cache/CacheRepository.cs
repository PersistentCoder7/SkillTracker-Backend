using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkillTracker.Search.Cache.Interfaces;
using SkillTracker.Search.Domain.Models;
using StackExchange.Redis;

namespace SkillTracker.Search.Cache
{
    public class CacheRepository:ICacheRepository
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<CacheRepository> _logger;

        private string redisKey = "cached_profiles";

        public CacheRepository(IConnectionMultiplexer redis,ILogger<CacheRepository> logger)
        {
            _redis = redis;
            _logger = logger;
        }
        public async Task<List<CachedProfile>> Read()
        {
            _logger.LogInformation("Redis:Cache Read");
            var result = new List<CachedProfile>();

            var db = _redis.GetDatabase();
            var value= await db.StringGetAsync(redisKey);
            if (!string.IsNullOrEmpty(value))
            {
                result = JsonConvert.DeserializeObject<List<CachedProfile>>(value.ToString());
            }

            
            return await Task.FromResult(result);
        }

        public async Task Write(string json)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(redisKey, json);
            _logger.LogInformation("Redis:Cache refreshed");
        }
    }
}
