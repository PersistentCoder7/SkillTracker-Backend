using System.Net;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Profile.Api.ActionFilters;
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

        /// <summary>
        /// Retrieve associate's profile based on the associateID.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Domain.Models.Profile), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Domain.Models.Profile), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Domain.Models.Profile>> GetProfile(string id)
        {
            var profile = await _profileService.GetProfile(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        /// <summary>
        /// Add a new associate profile.
        /// </summary>
        /// <response code="200">The new profile is added successfully</response>
        /// <response code="201">The new profile is added successfully</response>
        /// <response code="400">Unable to add the profile due to validation error</response>
        /// <param name="addProfileDto"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ValidateDtoAttribute]
        public IActionResult Post([FromBody] AddProfileDTO addProfileDto)
        {
            _profileService.AddProfile(addProfileDto);
            return Ok(addProfileDto);
        }
        /// <summary>
        /// Updates the profile of an Associate.
        /// </summary>['
        /// <response code="204">The profile was updated successfully</response>
        /// <response code="404">The AssociateId is Invalid</response>
        /// <response code="400">Unable to update the profile due to validation error</response>
        /// <param name="updateProfileDto"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// <exception cref="SkillTrackerDomainException"></exception>
        [HttpPut(Name = "UpdateProfile")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        [ValidateDtoAttribute]
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
            
            return NoContent();
        }
    }
}