using System.ComponentModel.DataAnnotations;
using SkillTracker.Profile.Application.ModelValidators;

namespace SkillTracker.Profile.Application.Models
{
    public class AddProfileDTO
    {
        [Required]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "AssociateId must have a minimum of 5 characters")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "AssociateId can have a maximum of 30 characters")]
        [StartsWith("CTS")]
        //[MinLength(5, ErrorMessage = "AssociateId must have a minimum of 5 characters")]
        public string AssociateId { get; set; }

        [RegularExpression(@"^.{5,}$", ErrorMessage = "Name must have a minimum of 5 characters")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Name can have a maximum of 30 characters")]
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        
        [Required]
        [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Mobile number must be exactly 10 digits and cannot start with 0")]
        public string Mobile { get; set; }
        [Required]
        [AtLeastOneSkill(ErrorMessage = "At least one skill record is required.")]
        [AtLeastOneSkillWithProficiency(ErrorMessage = "At least one skill record with proficiency greater than 0 is required.")]
        public List<AddSkillsDTO> Skills { get; set; }
    }
}
