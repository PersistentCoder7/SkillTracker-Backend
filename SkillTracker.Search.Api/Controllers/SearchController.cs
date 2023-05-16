using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Common.Utils.Exceptions;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Services.Profile.Commands;
using SkillTracker.Search.Api.ActionFilters;
using SkillTracker.Search.Api.Models;
using SkillTracker.Search.Application.Services.Search.Commands;
using SkillTracker.Search.Domain.Models;
using SkillTracker.Search.Domain.Models.SkillTracker.Search.Api.Models;

namespace SkillTracker.Search.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("skill-tracker/api/v{version:apiVersion}/admin")]
[Produces("application/json")]
public class SearchController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SearchController> _logger;

    public SearchController(IMediator mediator, ILogger<SearchController> logger)
    {
        _mediator = mediator;
        _logger = logger;
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
        var response = await SearchProfileAsync(request);
       
        
        return await Task.FromResult(response);
    }
    
    private async Task<List<CachedProfile>> SearchProfileAsync(SearchProfileRequest request) =>
       await _mediator.Send(new SearchProfileCommand(new SearchCriteria(request.AssociateId, request.Name, request.SkillId)));
}