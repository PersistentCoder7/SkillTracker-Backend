using SkillTracker.Search.Application.Interfaces;
using SkillTracker.Search.Cache.Interfaces;
using SkillTracker.Search.Domain.Models;
using SkillTracker.Search.Domain.Models.SkillTracker.Search.Api.Models;

namespace SkillTracker.Search.Application.Services;

public class SearchService : ISearchService
{
    private readonly ICacheRepository _repository;

    public SearchService(ICacheRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CachedProfile>> Search(SearchCriteria searchCriteria)
    {
        var cachedProfiles = await _repository.Read();

        var result = new List<CachedProfile>();

        
        if (!string.IsNullOrEmpty(searchCriteria.AssociateId))
        {
            //_logger.LogInformation($"Search profile in CosmosDB for Associate: {AssociateId}");
            var x =
                cachedProfiles.FirstOrDefault(s => s.AssociateId.ToLower() == searchCriteria.AssociateId.ToLower());
            if (x != null) result.Add(x);
        }
        else if (!string.IsNullOrEmpty(searchCriteria.Name))
        {
            var y = cachedProfiles
                .Where(s => s.Name.ToLower().StartsWith(searchCriteria.Name.ToLower()))
                .ToList();
            if (y != null) result.AddRange(y);
        }
        else if (!string.IsNullOrEmpty(searchCriteria.Skill))
        {
            //var z = cachedProfiles
            //    .Where(c => c.Skills.Any(skill => skill.SkillId == searchCriteria.Skill) && skill.Proficiency > 10))
            //    .ToList();
            //if (z != null) result.AddRange(z);
        }

        result.ForEach(x => x.Skills = x.Skills.OrderByDescending(y => y.Proficiency).ToList());
        return await Task.FromResult(result);
    }
}