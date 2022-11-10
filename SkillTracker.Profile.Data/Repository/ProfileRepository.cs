using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillTracker.Profile.Data.DbContext;
using SkillTracker.Profile.Domain.Events;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Profile.Data.Repository
{
    public  class ProfileRepository: IProfileRepository
    {
        private readonly ProfileDbContext _profileDbContext;
        private readonly ILogger<ProfileDbContext> _logger;

        public ProfileRepository(ProfileDbContext profileDbContext, ILogger<ProfileDbContext> logger)
        {
            _profileDbContext = profileDbContext;
            _logger = logger;
        }

        public async Task<Domain.Models.Profile> SaveProfile(Domain.Models.Profile profile)
        {
            if (profile.UpdatedOn != null)
            {
                
                _profileDbContext.Entry(profile).State = EntityState.Modified;
            }
            else
            {
                _profileDbContext.Profiles.Add(profile);
            }
            _logger.LogInformation($"Save profile in CosmosDB for Associate: {profile.AssociateId}");
            await _profileDbContext.SaveChangesAsync();
            _logger.LogInformation($"Saved profile in CosmosDB for Associate: {profile.AssociateId}");
            return await Task.FromResult(profile);
        }

        public async Task<IEnumerable<Domain.Models.Profile>> GetAllProfiles()
        {
            return await _profileDbContext.Profiles.ToListAsync();
        }

        public async Task<Domain.Models.Profile> GetProfile(string id)
        {
            return await _profileDbContext.Profiles.FirstOrDefaultAsync(p => p.AssociateId == id);
        }

        public async Task<List<Domain.Models.Profile>> Search(SearchProfileEvent @event)
        {
            var result = new List<Domain.Models.Profile>();
          

            if (@event.AssociateId != null)
            {
                //_logger.LogInformation($"Search profile in CosmosDB for Associate: {AssociateId}");
                var x=_profileDbContext.Profiles.AsEnumerable()
                    .FirstOrDefault(s => s.AssociateId.ToLower() == @event.AssociateId.ToLower());
                if (x!=null) result.Add(x);
            }
            else if(@event.Name !=null)
            {
                var y =  _profileDbContext.Profiles.AsEnumerable()
                    .Where(s => s.Name.ToLower().StartsWith(@event.Name.ToLower())).ToList();
                if (y!=null) result.AddRange(y);
            }
            else if (@event.Skill != null)
            {
                var z= _profileDbContext.Profiles.AsEnumerable().Where(c => c.Skills.Any(skill => skill.Name.Equals(@event.Skill, StringComparison.OrdinalIgnoreCase))).ToList();
                if (z!=null) z.AddRange(z);
            }
            
            result.ForEach(x=> x.Skills = x.Skills.OrderByDescending(y=> y.Proficiency).ToList());
            return await Task.FromResult(result);
        }
    }
}
