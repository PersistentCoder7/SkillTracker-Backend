namespace SkillTracker.Profile.Infrastructure.Interfaces;

public interface IProfileRepository
{
    Task<Domain.Models.Profile> AddProfile(Domain.Models.Profile profile);
    Task<Domain.Models.Profile> UpdateProfile(Domain.Models.UpdateProfile profile);
    Task<Domain.Models.Profile> GetProfile(string id);
}