using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Domain.Models;
using SkillTracker.Profile.Infrastructure.Interfaces;

namespace SkillTracker.Profile.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepository;
    /*IProfileRepository profileRepository, IEventBus bus*/
    public ProfileService(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }
    //public async Task<IEnumerable<Domain.Models.Profile>> GetProfiles()
    //{
    //    return await _profileRepository.GetAllProfiles();
    //}


    public void AddProfile(Domain.Models.Profile profile)
    {
      
    }

    public async Task<Domain.Models.Profile> GetProfile(string id)
    {
        return await _profileRepository.GetProfile(id);
    }

    public void UpdateProfile(UpdateProfile profile)
    {
      
    }
}