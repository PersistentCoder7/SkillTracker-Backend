using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Profile.Domain.Events;

namespace SkillTracker.Profile.Domain.Interfaces
{
    public  interface IAdminRepository
    {
        Task<List<Models.Profile>> Search(SearchProfileEvent @event);
    }
}
