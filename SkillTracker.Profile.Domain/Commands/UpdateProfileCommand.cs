using SkillTracker.Domain.Core.Commands;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Domain.Commands
{
    public  class UpdateProfileCommand:Command
    {
        public string AssociateId { get; set; }
        
        public List<Skill> Skills { get; set; }
        public UpdateProfileCommand()
        {
            
        }
    }
}
