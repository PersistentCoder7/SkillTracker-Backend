using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Common.Utils.Exceptions;
using SkillTracker.Profile.Api.ActionFilters;
using SkillTracker.Profile.Api.Models;
using SkillTracker.Profile.Application.Services.Profile.Commands;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Api.Controllers;

[ApiVersion("1.0")]
[Route("skill-tracker/api/v{version:apiVersion}/engineer")]
[Produces("application/json")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(IMediator mediator, ILogger<ProfileController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Retrieve associate's profile based on the associateID.
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [CustomErrorMessage("An error occurred while processing your request.")]
    public async Task<ActionResult<Domain.Models.Profile>> GetProfile(string id)
    {
        _logger.LogInformation("GetProfile is invoked");
        Domain.Models.Profile profile = await GetProfileAsync(id);
        return Ok(profile);
    }

    private async Task<Domain.Models.Profile> GetProfileAsync(string associateId) =>
        await _mediator.Send(new GetProfileCommand(associateId));

    /// <summary>
    /// Add associates profile.
    /// </summary>
    /// <returns></returns>
    [HttpPost(Name = "AddProfile")]
    [ValidateDto]
    [CustomErrorMessage("An error occurred while processing your request.", 500)]
    public async Task<IActionResult> Post([FromBody] AddProfileRequest request)
    {
        _logger.LogInformation("AddProfile is invoked");
        await AddProfileAsync(request: request);
        return Ok(request);
    }

    private async Task AddProfileAsync(AddProfileRequest request) =>
        await _mediator.Send(new AddProfileCommand(request.AssociateId,
            request.Name,
            request.Email,
            request.Mobile,
            Skills:request.Skills.
                Select(x=> new SkillProficiency(){SkillId = x.SkillId, Proficiency = x.Proficiency}).ToList())
        );

    /// <summary>
    /// Update the associate's profile. Only skills can be updated.
    /// </summary>
    /// <returns></returns>
    [HttpPut(Name = "UpdateProfile")]
    [ValidateDto]
    [CustomErrorMessage("An error occurred while processing your request.", 500)]
    public async Task<ActionResult<int>> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        _logger.LogInformation("UpdateProfile is invoked");
        await UpdateProfileAsync(request: request);
        return NoContent();
    }

    private async Task UpdateProfileAsync(UpdateProfileRequest request) =>
        await _mediator.Send(new UpdateProfileCommand(request.AssociateId, 
            Skills: request.Skills.
            Select(x => new SkillProficiency() { SkillId = x.SkillId, Proficiency = x.Proficiency }).ToList())
        );
}