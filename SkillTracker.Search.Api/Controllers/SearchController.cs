using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Common.Utils.Exceptions;
using SkillTracker.Search.Api.ActionFilters;
using SkillTracker.Search.Api.Models;
using SkillTracker.Search.Application.Interfaces;
using SkillTracker.Search.Domain.Models;
using SkillTracker.Search.Domain.Models.SkillTracker.Search.Api.Models;

namespace SkillTracker.Search.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("skill-tracker/api/v{version:apiVersion}/admin")]
[Produces("application/json")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISearchService _service;

    public SearchController(ISearchService service, ILogger<SearchController> logger)
    {
        
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Search the cached profiles based on SkillId, Associate name or Associate ID 
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Returns the list of profiles matching the search criteria</returns>
    [HttpPost("search", Name = "Search")]
    [ValidateDto]
    [CustomErrorMessage("An error occurred while processing your search request.",500)]
    public async Task<List<CachedProfile>> Search([FromBody]SearchProfileRequest request)
    {
        _logger.LogInformation("Search is invoked");
        //var response = await SearchProfileAsync(request);
        var response = await _service.Search(new SearchCriteria(request.AssociateId, request.Name, request.SkillId));


        return await Task.FromResult(response);
    }
}