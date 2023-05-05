using SkillTracker.Domain.Core.Commands;

namespace SkillTracker.Profile.Domain.Commands
{
    public  class GetProfileCommand:Command
    {
        public string AssociateId { get; set; }
    }
}
