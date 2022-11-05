using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Domain.Core.Commands;

namespace SkillTracker.Profile.Domain.Commands
{
    public  class GetProfileCommand:Command
    {
        public string AssociateId { get; set; }
    }
}
