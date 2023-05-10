using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Profile.Api.Models;

public class UpdateProfileRequest
{
    [Required]
    public string AssociateId { get; set; }
    [Required]
    [ModelValidators.AtLeastOneSkill(ErrorMessage = "At least one skill record is required.")]
    [ModelValidators.AtLeastOneSkillWithProficiency(ErrorMessage = "At least one skill record with proficiency greater than 0 is required.")]
    public List<Skills> Skills { get; set; }
}