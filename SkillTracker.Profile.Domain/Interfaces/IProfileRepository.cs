﻿using SkillTracker.Profile.Domain.Events;

namespace SkillTracker.Profile.Domain.Interfaces;

public  interface IProfileRepository
{
    Task<Models.Profile> SaveProfile(Models.Profile profile);
    Task<IEnumerable<Models.Profile>> GetAllProfiles();
    Task<Models.Profile> GetProfile(string id);

    Task<List<Models.Profile>> Search(SearchProfileEvent @event);
}