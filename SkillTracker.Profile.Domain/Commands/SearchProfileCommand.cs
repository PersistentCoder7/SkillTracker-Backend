using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Domain.Core.Commands;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Domain.Commands
{
    public  class SearchProfileCommand:Command
    {
        public string AssociateId { get; set; }
        public string Name { get; set; }
        public string Skill { get; set; }
        public SearchProfileCommand()
        {
            
        }
    }
}
