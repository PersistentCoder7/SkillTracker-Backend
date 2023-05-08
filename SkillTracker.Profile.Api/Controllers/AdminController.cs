using System.Net;

using Microsoft.AspNetCore.Mvc;
using SkillTracker.Profile.Api.ActionFilters;
using SkillTracker.Profile.Api.Infrastructure.Exceptions;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Models;

namespace SkillTracker.Profile.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("skill-tracker/api/v{version:apiVersion}/admin")]
    [Produces("application/json")]
    public class AdminController : ControllerBase
    {
        private readonly IProfileService _profileService;


        private readonly ILogger<AdminController> _logger;

        public AdminController(IProfileService profileService, ILogger<AdminController> logger)
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
        /// <summary>
        /// Search associate profile based on AssociateID, Skill and Associate name.
        /// </summary>
        /// <param name="searchProfileDto"></param>
        /// <returns>200</returns>
        [HttpPost("search", Name = "Search")]
        [ValidateDtoAttribute]
        [CustomErrorMessage("An error occurred while processing your request.",500)]
        public async Task<List<Domain.Models.Profile>> Search(SearchProfileDTO searchProfileDto)
        {
            _logger.LogInformation("Performing the admin search functionality");
            var result= await _profileService.Search(searchProfileDto);
            return result;
        }


    }
}