using SkillTracker.Profile.Domain.Events;

namespace SkillTracker.Profile.Domain.Interfaces;

public  interface IAdminRepository
{
    Task<List<Models.Profile>> Search(SearchProfileEvent @event);
}