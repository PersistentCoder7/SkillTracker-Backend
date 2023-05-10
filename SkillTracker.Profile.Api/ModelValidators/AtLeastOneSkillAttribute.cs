using System.ComponentModel.DataAnnotations;
using SkillTracker.Profile.Api.Models;

namespace SkillTracker.Profile.Api.ModelValidators;

public class AtLeastOneSkillAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var skills = value as List<Skills>;
        if (skills == null || skills.Count == 0)
        {
            return new ValidationResult("At least one skill record is required.");
        }
        return ValidationResult.Success;
    }
}