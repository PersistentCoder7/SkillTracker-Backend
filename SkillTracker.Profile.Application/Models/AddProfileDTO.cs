using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Profile.Application.ModelValidators;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Models
{
    public class AddProfileDTO
    {
        [Required]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "AssociateId must have a minimum of 5 characters")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "AssociateId can have a maximum of 30 characters")]
        [StartsWith("CTS")]
        public string AssociateId { get; set; }

        [RegularExpression(@"^.{5,}$", ErrorMessage = "Name must have a minimum of 5 characters")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Name can have a maximum of 30 characters")]
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        
        [Required]
        public string Mobile { get; set; }
        [Required]
        public List<AddSkillsDTO> Skills { get; set; }
    }
}
