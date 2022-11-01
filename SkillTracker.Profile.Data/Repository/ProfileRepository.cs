using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Profile.Data.DbContext;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Profile.Data.Repository
{
    public  class ProfileRepository: IProfileRepository
    {
        private readonly ProfileDbContext _profileDbContext;

        public ProfileRepository(ProfileDbContext profileDbContext)
        {
                _profileDbContext = profileDbContext;
        }
        public async Task<IEnumerable<Domain.Models.Profile>> GetAllProfiles()
        {
            return await _profileDbContext.Profiles.ToListAsync();
        }
    }
}
