using System.ComponentModel.DataAnnotations;
using SkillTracker.Profile.Api.Models;

namespace SkillTracker.Profile.Api.ModelValidators;

public class AtLeastOneSkillWithProficiencyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var skills = value as List<Skills>;
        if (skills == null || skills.All(s => s.Proficiency == 0))
        {
            return new ValidationResult("At least one skill record with proficiency greater than 0 is required.");
        }
        return ValidationResult.Success;
    }
}