using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Profile.Application.Models;

namespace SkillTracker.Profile.Application.Interfaces
{
    public interface IAdminService
    {
        public Task<List<Domain.Models.Profile>> Search(SearchProfileDTO searchProfileDto);
    }
}
