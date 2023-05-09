using SkillTracker.Domain.Core.Commands;

namespace SkillTracker.Profile.Domain.Commands;

public  class SearchProfileCommand:Command
{
    public string AssociateId { get; set; }
    public string Name { get; set; }
    public string Skill { get; set; }
    public SearchProfileCommand()
    {
            
    }
}