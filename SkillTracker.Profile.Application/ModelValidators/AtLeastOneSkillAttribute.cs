using SkillTracker.Profile.Application.Models;
using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Profile.Application.ModelValidators
{
    public class AtLeastOneSkillAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var skills = value as List<AddSkillsDTO>;
            if (skills == null || skills.Count == 0)
            {
                return new ValidationResult("At least one skill record is required.");
            }
            return ValidationResult.Success;
        }
    }
}
