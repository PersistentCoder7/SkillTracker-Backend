using SkillTracker.Profile.Application.Services.Profile.Commands;

namespace SkillTracker.Profile.Application.Interfaces;

public interface IProfileService
{
    public Task<IEnumerable<Domain.Models.Profile>> GetProfiles();
    //public void AddProfile(AddProfileDTO addProfileDto);
    public Task<Domain.Models.Profile> GetProfile(string id);
    public void UpdateProfile(UpdateProfileCommand updateProfileDto);
}