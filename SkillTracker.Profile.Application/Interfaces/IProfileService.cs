using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Interfaces;

public interface IProfileService
{
    public void AddProfile(Domain.Models.Profile profile);
    public Task<Domain.Models.Profile> GetProfile(string id);
    public void UpdateProfile(UpdateProfile profile);
}