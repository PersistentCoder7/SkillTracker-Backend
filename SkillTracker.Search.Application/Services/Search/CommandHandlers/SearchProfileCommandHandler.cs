using MediatR;
using SkillTracker.Search.Application.Interfaces;
using SkillTracker.Search.Application.Services.Search.Commands;
using SkillTracker.Search.Domain.Models;

namespace SkillTracker.Search.Application.Services.Search.CommandHandlers;

public class SearchProfileCommandHandler : IRequestHandler<SearchProfileCommand, List<CachedProfile>>
{
    private readonly ISearchService _searchService;

    public SearchProfileCommandHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }
    public async Task<List<CachedProfile>> Handle(SearchProfileCommand request, CancellationToken cancellationToken)
    {
        return await _searchService.Search(request.searchCriteria);
    }
}