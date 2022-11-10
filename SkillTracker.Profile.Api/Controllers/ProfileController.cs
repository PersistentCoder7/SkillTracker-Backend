using System.Net;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Profile.Api.Infrastructure.Exceptions;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Models;

namespace SkillTracker.Profile.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("skill-tracker/api/v{version:apiVersion}/engineer")]
    [Produces("application/json")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;


        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
        {
            this._profileService = profileService;
            _logger = logger;
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Domain.Models.Profile), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Domain.Models.Profile), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Domain.Models.Profile>> GetProfile(string id)
        {
            var profile = await _profileService.GetProfile(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }
        
        [HttpPost(Name = "AddProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] AddProfileDTO addProfileDto)
        {
            _profileService.AddProfile(addProfileDto);
            return Ok(addProfileDto);
        }

        [HttpPut(Name = "UpdateProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDto, [FromHeader(Name = "x-userid")] string userid)
        {
            if (string.IsNullOrWhiteSpace(userid))
            {
                throw new SkillTrackerDomainException("UserId header is missing");
                //var errors = new List<ValidationFailure> { new ValidationFailure("", "UserId header missing") };
                //throw new FluentValidation.ValidationException(errors);
            }
            //TODO: Ensure that the UserId is not blank
            
            var associateId = userid;

            var profile = await _profileService.GetProfile(associateId);
            if (profile==null) return NotFound();
            
            //If the profile wasn't updated ever
            var currentDate=DateTime.Now;
            var updatedDate=(profile.UpdatedOn==null) ? currentDate: profile.UpdatedOn;

            if (currentDate.Subtract(updatedDate.Value).Days < 10 ||
                currentDate.Subtract(profile.AddedOn.Value).Days < 10)
            {
                throw new SkillTrackerDomainException("The profile can be updated only after 10 days of adding or updating the profile");
            }
            
            _profileService.UpdateProfile(updateProfileDto);
            
            return Ok();
        }
    }
}