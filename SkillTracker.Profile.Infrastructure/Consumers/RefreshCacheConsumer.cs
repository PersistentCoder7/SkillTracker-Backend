using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Infrastructure.Interfaces;
using SkillTracker.Search.Cache.Interfaces;
using SkillTracker.Search.Domain.Models;

namespace SkillTracker.Profile.Infrastructure.Consumers
{
    public class RefreshCacheEventConsumer: IConsumer<RefreshCacheEvent>
    {
        private readonly IProfileRepository _repository;

        private readonly ILogger<RefreshCacheEventConsumer> _logger;
        private readonly ICacheRepository _cache ;

        public RefreshCacheEventConsumer(IProfileRepository repository,ILogger<RefreshCacheEventConsumer> logger,ICacheRepository cache)
        {
            _repository = repository;
            _logger = logger;
            _cache = cache;
        }
        public async Task Consume(ConsumeContext<RefreshCacheEvent> context)
        {
            var profiles = await _repository.GetProfiles();
            
            string json = JsonConvert.SerializeObject(profiles);
            _logger.LogInformation(json); 
            await _cache.Write(json);
        }
    }
}
