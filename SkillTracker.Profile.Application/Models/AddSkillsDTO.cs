using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Models
{
    public class AddSkillsDTO
    {
        [Required(ErrorMessage = "Skill Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proficiency is required")]
        [Range(1, 20, ErrorMessage = "Invalid Proficiency: Range 1 to 20")]
        public int Proficiency { get; set; }
    }
}
