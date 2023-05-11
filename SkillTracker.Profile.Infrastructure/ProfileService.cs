using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Domain.Models;
using SkillTracker.Profile.Infrastructure.Interfaces;

namespace SkillTracker.Profile.Infrastructure;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repository;

    /*IProfileRepository profileRepository, IEventBus bus*/
    public ProfileService(IProfileRepository repository)
    {
        _repository = repository;
    }
    //public async Task<IEnumerable<Domain.Models.Profile>> GetProfiles()
    //{
    //    return await _profileRepository.GetAllProfiles();
    //}


    public async Task AddProfile(Domain.Models.Profile profile)
    {
        await _repository.AddProfile(profile);
    }

    public async Task<Domain.Models.Profile> GetProfile(string id)
    {
        return await _repository.GetProfile(id);
    }

    public async Task UpdateProfile(UpdateProfile profile)
    {
        await _repository.UpdateProfile(profile);
    }
}