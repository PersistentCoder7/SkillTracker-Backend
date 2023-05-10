using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Search.Domain.Models
{
    namespace SkillTracker.Search.Api.Models
    {
        public record SearchCriteria(string AssociateId, string Name, string Skill);
    }
}
