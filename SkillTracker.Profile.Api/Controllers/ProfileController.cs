using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Models;

namespace SkillTracker.Profile.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("skill-tracker/api/v{version:apiVersion}/engineer")]
    [Produces("application/json")]
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
        public IActionResult Post([FromBody] AddProfileDTO addProfileDto)
        {
            _profileService.AddProfile(addProfileDto);
            return Ok(addProfileDto);
        }

        [HttpPut(Name = "UpdateProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDto, [FromHeader(Name = "x-userid")] string userid)
        {
            //if (string.IsNullOrWhiteSpace(userId))
            //{
            //    var errors = new List<ValidationFailure> { new ValidationFailure("", "UserId header missing") };
            //    throw new FluentValidation.ValidationException(errors);
            //}
            //TODO: Ensure that the UserId is not blank
            userid = "2";
            var associateId = userid;

            var profile = await _profileService.GetProfile(associateId);
            if (profile==null) return NotFound();
            
            _profileService.UpdateProfile(updateProfileDto);
            
            return Ok();
        }
    }
}