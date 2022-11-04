using Microsoft.AspNetCore.Mvc;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Models;

namespace SkillTracker.Profile.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;


        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
        {
            this._profileService = profileService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Models.Profile>>> Get()
        {
            var profiles = await _profileService.GetProfiles();
            return Ok(profiles);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddProfileDTO addProfileDto)
        {
            _profileService.AddProfile(addProfileDto); 
            return Ok(addProfileDto);
        }
    }
}