using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Profile.Application.Models
{
    public class AddSkillsDTO
    {
        [Required(ErrorMessage = "Skill Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proficiency is required")]
        [Range(0, 20, ErrorMessage = "Invalid Proficiency: Range 1 to 20")]
        public int Proficiency { get; set; }

        public bool IsTechnical { get; set; }
        public int SkillId { get; set; }
    }
}
