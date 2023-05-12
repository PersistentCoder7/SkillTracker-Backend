using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Profile.Api.Models;

public class Skills
{
    [Required(ErrorMessage = "Proficiency is required")]
    [Range(0, 20, ErrorMessage = "Invalid Proficiency: Range 0 to 20")]
    public int Proficiency { get; set; }

    [Required(ErrorMessage = "SkillId is required")]
    [Range(1, 13, ErrorMessage = "Invalid SkillId: Range 1 to 13")]
    public int SkillId { get; set; }
}