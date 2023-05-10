using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Profile.Domain.Models;

public class Profile
{
    [Key] public string AssociateId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public DateTime? AddedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public List<BasicSkill> Skills { get; set; }
}

public class UpdateProfile
{
    public string AssociateId { get; set; }
    public List<BasicSkill> Skills { get; set; }
}
public class BasicSkill
{
    public int SkillId { get; set; }
    public int Proficiency { get; set; }
}

public class Skill
{
    public string ProfileAssociateId { get; set; }
    public int SkillId { get; set; }
    public bool IsTechnical { get; set; }
    public string Name { get; set; }
    public int Proficiency { get; set; }
}