using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Interfaces;

public interface IProfileService
{
    public Task AddProfile(Domain.Models.Profile profile);
    public Task<Domain.Models.Profile> GetProfile(string id);
    public Task UpdateProfile(UpdateProfile profile);
}