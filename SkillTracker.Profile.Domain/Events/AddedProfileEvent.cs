using SkillTracker.Domain.Core.Events;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Domain.Events;

public class AddedProfileEvent: Event
{
        
    public string AssociateId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
    public string Mobile { get; set; }

    public DateTime? AddedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public List<Skill> Skills { get; set; }

    public AddedProfileEvent()
    {
        AddedOn=DateTime.Now;
        UpdatedOn = null;
    }
}