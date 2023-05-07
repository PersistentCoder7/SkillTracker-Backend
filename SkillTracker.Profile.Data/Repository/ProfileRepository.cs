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
            return await _profileDbContext.Profiles.FirstOrDefaultAsync(p => p.AssociateId.ToLower() == id.ToLower());
        }

        public async Task<List<Domain.Models.Profile>> Search(SearchProfileEvent @event)
        {
            var result = new List<Domain.Models.Profile>();

            var profileList = _profileDbContext.Profiles.AsEnumerable();
            if (!string.IsNullOrEmpty(@event.AssociateId))
            {
                //_logger.LogInformation($"Search profile in CosmosDB for Associate: {AssociateId}");
                var x=
                    profileList.FirstOrDefault(s => s.AssociateId.ToLower() == @event.AssociateId.ToLower());
                if (x!=null) result.Add(x);
            }
            else if(!string.IsNullOrEmpty(@event.Name))
            {
                var y = profileList
                    .Where(s => s.Name.ToLower().StartsWith(@event.Name.ToLower()))
                    .ToList();
                if (y!=null) result.AddRange(y);
            }
            else if (!string.IsNullOrEmpty(@event.Skill))
            {
                var z= profileList
                    .Where(c => c.Skills.Any(skill => skill.Name.Equals(@event.Skill, StringComparison.OrdinalIgnoreCase) && skill.Proficiency >10))
                    .ToList();
                if (z!=null) result.AddRange(z);
            }
            
            result.ForEach(x=> x.Skills = x.Skills.OrderByDescending(y=> y.Proficiency).ToList());
            return await Task.FromResult(result);
        }
    }
}
