using Microsoft.Extensions.Logging;
using SkillTracker.Search.Application.Interfaces;
using SkillTracker.Search.Cache.Interfaces;
using SkillTracker.Search.Domain.Models;
using SkillTracker.Search.Domain.Models.SkillTracker.Search.Api.Models;

namespace SkillTracker.Search.Application.Services;

public class SearchService : ISearchService
{
    private readonly ICacheRepository _repository;
    private readonly ILogger<SearchService> _logger;

    public SearchService(ICacheRepository repository, ILogger<SearchService> logger)
    {
        _repository = repository;
        _logger = logger;
        _logger.LogInformation("Created an instance of SearchService");
    }

    public async Task<List<CachedProfile>> Search(SearchCriteria searchCriteria)
    {
        var cachedProfiles = await _repository.Read();

        var result = new List<CachedProfile>();

        
        if (!string.IsNullOrEmpty(searchCriteria.AssociateId))
        {
            _logger.LogInformation($"Cache search where AssociateID: {searchCriteria.AssociateId}");
            var x =
                cachedProfiles.FirstOrDefault(s => s.AssociateId.ToLower() == searchCriteria.AssociateId.ToLower());
            if (x != null) result.Add(x);
        }
        else if (!string.IsNullOrEmpty(searchCriteria.Name))
        {
            _logger.LogInformation($"Cache search where Associate name starts with: {searchCriteria.Name}");
            var y = cachedProfiles
                .Where(s => s.Name.ToLower().StartsWith(searchCriteria.Name.ToLower()))
                .ToList();
            if (y != null) result.AddRange(y);
        }
        else if (searchCriteria.SkillId>0)
        {
            _logger.LogInformation($"Cache search where SkillId : {searchCriteria.SkillId} and proficiency is greater than 10");
            var z = cachedProfiles
                .Where(c => c.Skills.Any(skill => skill.SkillId == searchCriteria.SkillId && skill.Proficiency > 10))
                .ToList();
            if (z != null) result.AddRange(z);
        }

        result.ForEach(x => x.Skills = x.Skills.OrderByDescending(y => y.Proficiency).ToList());
        return await Task.FromResult(result);

    }
}