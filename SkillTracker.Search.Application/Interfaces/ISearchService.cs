using SkillTracker.Search.Domain.Models.SkillTracker.Search.Api.Models;

namespace SkillTracker.Search.Application.Interfaces;

public interface ISearchService
{
    public Task<List<Domain.Models.CachedProfile>> Search(SearchCriteria searchCriteria);
}