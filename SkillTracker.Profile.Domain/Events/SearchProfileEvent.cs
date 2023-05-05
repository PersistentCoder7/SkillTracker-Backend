using SkillTracker.Domain.Core.Events;

namespace SkillTracker.Profile.Domain.Events
{
    public class SearchProfileEvent: Event
    {
        public string AssociateId { get; set; }
        public string Name { get; set; }
        public string Skill { get; set; }

    }
}
