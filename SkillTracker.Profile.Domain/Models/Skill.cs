namespace SkillTracker.Profile.Domain.Models;

public class Skill
{
    public string ProfileAssociateId { get; set; }
    public int SkillId { get; set; }
    public bool IsTechnical { get; set; }
    public string Name { get; set; }
    public int Proficiency { get; set; }
}