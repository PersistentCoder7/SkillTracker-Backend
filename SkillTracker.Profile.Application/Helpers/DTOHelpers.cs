using SkillTracker.Profile.Application.Models;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Helpers
{
    public static class DTOHelpers
    {
        public static List<Skill> GetSkills(this List<AddSkillsDTO> skillsDtos, string associateId) 
        {
            var skills = new List<Skill>()
            {
                new Skill()
                {
                    IsTechnical = true, Name = "HTML-CSS-Javascript", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 1
                },
                new Skill() { IsTechnical = true, Name = "Angular", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 2  },
                new Skill() { IsTechnical = true, Name = "React", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 3 },
                new Skill()
                {
                    IsTechnical = true, Name = "Asp.Net Core", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 4
                },
                new Skill() { IsTechnical = true, Name = "Restful", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 5 },
                new Skill()
                {
                    IsTechnical = true, Name = "Entity Framework", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 6
                },
                new Skill() { IsTechnical = true, Name = "Git", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 7 },
                new Skill() { IsTechnical = true, Name = "Docker", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 8 },
                new Skill() { IsTechnical = true, Name = "Jenkins", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 9 },
                new Skill() { IsTechnical = true, Name = "Azure", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 10 },
                new Skill() { IsTechnical = false, Name = "Spoken", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 11 },
                new Skill()
                {
                    IsTechnical = false, Name = "Communication", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 12
                },
                new Skill() { IsTechnical = false, Name = "Aptitude", Proficiency = 0, ProfileAssociateId = associateId,SkillId = 13 }
            };
            skills.ForEach(x =>
            {
                var o = skillsDtos.Where(s => s.Name == x.Name).FirstOrDefault();
                if (o != null)
                {
                    x.Proficiency = o.Proficiency;
                }
            });

            return skills;
        }
    }
}
