using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Profile.Application.Interfaces
{
    public interface IProfileService
    {
        public Task<IEnumerable<Domain.Models.Profile>> GetProfiles();
    }
}
