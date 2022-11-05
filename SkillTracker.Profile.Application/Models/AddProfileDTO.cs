using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Models
{
    public class AddProfileDTO
    {
        [Required]
        public string AssociateId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public List<Skill> Skills { get; set; }
    }
}
