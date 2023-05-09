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
        [CustomErrorMessage("An error occurred while processing your request.")]
        public async Task<ActionResult<Domain.Models.Profile>> GetProfile(string id)
        {
            var profile = await _profileService.GetProfile(id);
            if (profile == null)
            {
                throw new CustomErrorException("The associate profile is not found.", (int)StatusCodes.Status404NotFound);
            }
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
        [CustomErrorMessage("An error occurred while processing your request.", 500)]
        public async Task<IActionResult> Post([FromBody] AddProfileDTO addProfileDto)
        {
            var profile = await _profileService.GetProfile(addProfileDto.AssociateId);
            if (profile != null)
            {
                throw new CustomErrorException("The associate profile already exists.", (int)StatusCodes.Status409Conflict);
            }
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
        /// <returns></returns>
        /// <exception cref="CustomErrorException"></exception>
        [HttpPut(Name = "UpdateProfile")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        [ValidateDtoAttribute]
        [CustomErrorMessage("An error occurred while processing your request.", 500)]
        public async Task<ActionResult<int>> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDto)
        {
            //Check if the profile exists in the database
            var associateId = updateProfileDto.AssociateId;
            var profile = await _profileService.GetProfile(associateId);
            if (profile==null)
            {
                throw new CustomErrorException("The associate profile is not found.", (int)StatusCodes.Status404NotFound);
            }

            var currentDate=DateTime.Now;

            //If the profile wasn't updated ever
            if (profile.UpdatedOn==null && currentDate.Subtract(profile.AddedOn!.Value).Days <= 10)
            {
                throw new CustomErrorException("The profile can be updated only after 10 days of adding the profile", (int)StatusCodes.Status500InternalServerError);
            }
            else if (profile.UpdatedOn != null && currentDate.Subtract(profile.UpdatedOn!.Value).Days <= 10)
            {
                throw new CustomErrorException("The profile can be updated only after 10 days of updating the profile", (int)StatusCodes.Status500InternalServerError);
            }
            
            _profileService.UpdateProfile(updateProfileDto);
            
            return NoContent();
        }
    }
}