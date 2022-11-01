using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Profile.Application.Services
{
    public  class ProfileService: IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<IEnumerable<Domain.Models.Profile>> GetProfiles()
        {
            return await _profileRepository.GetAllProfiles();
        }
    }
}
