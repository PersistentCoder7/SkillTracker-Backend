namespace SkillTracker.Profile.Infrastructure.Interfaces;

public interface IProfileRepository
{
    Task<Domain.Models.Profile> SaveProfile(Domain.Models.Profile profile);
    Task<Domain.Models.Profile> GetProfile(string id);
}