using SkillTracker.Profile.Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Profile.Application.ModelValidators
{
    public class AtLeastOneSkillWithProficiencyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var skills = value as List<AddSkillsDTO>;
            if (skills == null || skills.All(s => s.Proficiency == 0))
            {
                return new ValidationResult("At least one skill record with proficiency greater than 0 is required.");
            }
            return ValidationResult.Success;
        }
    }

}
