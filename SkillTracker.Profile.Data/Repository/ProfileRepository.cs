using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillTracker.Profile.Data.DbContext;
using SkillTracker.Profile.Infrastructure.Interfaces;

namespace SkillTracker.Profile.Data.Repository;

public  class ProfileRepository: IProfileRepository
{
    private readonly ProfileDbContext _profileDbContext;
    private readonly ILogger<ProfileDbContext> _logger;

    public ProfileRepository(ProfileDbContext profileDbContext, ILogger<ProfileDbContext> logger)
    {
        _profileDbContext = profileDbContext;
        _logger = logger;
    }

    public async Task<Domain.Models.Profile> AddProfile(Domain.Models.Profile profile)
    {
        _profileDbContext.Profiles.Add(profile);
        _logger.LogInformation($"Save profile in CosmosDB for Associate: {profile.AssociateId}");
            await _profileDbContext.SaveChangesAsync();
        return await Task.FromResult(profile);
    }

    public async Task<Domain.Models.Profile> UpdateProfile(Domain.Models.UpdateProfile profile)
    {
        var dbProfile = await GetProfile(profile.AssociateId);
        _profileDbContext.Entry(dbProfile).State = EntityState.Modified;
       
        await _profileDbContext.SaveChangesAsync();
        _logger.LogInformation($"Updated profile in CosmosDB for Associate: {profile.AssociateId}");
        return await Task.FromResult(dbProfile);
    }

    //Will be used for refreshing the cache.
    public async Task<IEnumerable<Domain.Models.Profile>> GetAllProfiles()
    {
        return await _profileDbContext.Profiles.ToListAsync();
    }

    public async Task<Domain.Models.Profile> GetProfile(string id)
    {
        return await _profileDbContext.Profiles.FirstOrDefaultAsync(p => p.AssociateId.ToLower() == id.ToLower());
    }
}