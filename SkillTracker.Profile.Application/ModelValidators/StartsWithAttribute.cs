using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Profile.Application.ModelValidators;

public  class StartsWithAttribute: ValidationAttribute
{
    private readonly string _startsWith;

    public StartsWithAttribute(string startsWith)
    {
        _startsWith = startsWith;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string valueString && !valueString.StartsWith(_startsWith))
            return new ValidationResult($"{validationContext.MemberName} does not start with {_startsWith}");

        return ValidationResult.Success;
    }
}