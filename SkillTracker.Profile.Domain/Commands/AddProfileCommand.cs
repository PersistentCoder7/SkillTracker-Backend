using SkillTracker.Domain.Core.Commands;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Domain.Commands;

public  class AddProfileCommand:Command
{
    public string AssociateId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
    public string Mobile { get; set; }

    public List<Skill> Skills { get; set; }
    public AddProfileCommand()
    {
            
    }
}