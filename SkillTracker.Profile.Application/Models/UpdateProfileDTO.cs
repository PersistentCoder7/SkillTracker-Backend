using SkillTracker.Profile.Application.ModelValidators;
using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Profile.Application.Models
{
    public class UpdateProfileDTO
    {
        [Required]
        public string AssociateId { get; set; }
        [Required]
        [AtLeastOneSkill(ErrorMessage = "At least one skill record is required.")]
        [AtLeastOneSkillWithProficiency(ErrorMessage = "At least one skill record with proficiency greater than 0 is required.")]
        public List<AddSkillsDTO> Skills { get; set; }
    }
}
