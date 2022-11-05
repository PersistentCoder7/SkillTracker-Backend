using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Profile.Domain.Interfaces
{
    public  interface IProfileRepository
    {
        Task<Models.Profile> SaveProfile(Models.Profile profile);
        Task<IEnumerable<Models.Profile>> GetAllProfiles();
        //ProfileEntity GetProfile(string id);
        //void DeleteProfile(string id);
        //Task<ProfileEntity> UpdateProfile(ProfileEntity profile);
    }
}
