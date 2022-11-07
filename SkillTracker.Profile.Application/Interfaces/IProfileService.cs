using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Profile.Application.Models;

namespace SkillTracker.Profile.Application.Interfaces
{
    public interface IProfileService
    {
        public Task<IEnumerable<Domain.Models.Profile>> GetProfiles();
        public void AddProfile(AddProfileDTO addProfileDto);
        public Task<Domain.Models.Profile> GetProfile(string id);
        public void UpdateProfile(UpdateProfileDTO updateProfileDto);
    }
}
